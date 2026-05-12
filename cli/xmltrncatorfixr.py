#!/usr/bin/env python3
"""xmltrncatorfixr - cross-platform XML truncator-fixer.

Locates the first XML parser error using expat (built into Python), truncates
the file just before that point (minus a safety offset), then runs `xmllint
--recover` to add proper closing tags. No third-party Python dependencies.
"""
from __future__ import annotations

import argparse
import os
import shutil
import subprocess
import sys
from pathlib import Path
from xml.parsers import expat

__version__ = "1.0.0"


def first_error_byte(path: Path) -> tuple[int | None, str]:
    parser = expat.ParserCreate()
    data = path.read_bytes()
    try:
        parser.Parse(data, True)
    except expat.ExpatError as exc:
        return parser.ErrorByteIndex, str(exc)
    return None, "no parser error"


def find_xmllint() -> str | None:
    return shutil.which("xmllint")


def run_xmllint_recover(path: Path) -> int:
    xmllint = find_xmllint()
    if not xmllint:
        return 127
    tmp = path.with_suffix(path.suffix + ".recover.tmp")
    rc = subprocess.run(
        [xmllint, "--recover", "--output", str(tmp), str(path)],
        stdout=subprocess.DEVNULL,
        stderr=subprocess.DEVNULL,
    ).returncode
    if tmp.exists() and tmp.stat().st_size > 0:
        tmp.replace(path)
    elif tmp.exists():
        tmp.unlink()
    return rc


def resolve_output(input_path: Path, output: str | None, in_place: bool) -> Path:
    if in_place:
        return input_path
    if output:
        return Path(output)
    return input_path.with_name(f"truncated-fixed-{input_path.name}")


def main(argv: list[str] | None = None) -> int:
    p = argparse.ArgumentParser(
        prog="xmltrncatorfixr",
        description="Truncate an XML file just before its first parser error "
        "and rebuild closing tags with xmllint --recover.",
    )
    p.add_argument("input", help="path to a (possibly broken) XML file")
    p.add_argument("-n", "--offset", type=int, default=50,
                   help="bytes to back off before the error (default: 50)")
    p.add_argument("-o", "--output", help="output path")
    p.add_argument("-i", "--in-place", action="store_true",
                   help="overwrite the input file")
    p.add_argument("-q", "--quiet", action="store_true",
                   help="suppress progress messages")
    p.add_argument("-V", "--version", action="version",
                   version=f"xmltrncatorfixr {__version__}")
    args = p.parse_args(argv)

    src = Path(args.input)
    if not src.is_file():
        print(f"input file not found: {src}", file=sys.stderr)
        return 2

    dst = resolve_output(src, args.output, args.in_place)
    if dst != src:
        shutil.copyfile(src, dst)

    err_byte, msg = first_error_byte(dst)
    log = (lambda *a: None) if args.quiet else (
        lambda *a: print("[xmltrncatorfixr]", *a, file=sys.stderr))

    if err_byte is None:
        log(f"no parser error detected; copy written to {dst}")
        return 0

    log(f"first error at byte {err_byte}: {msg}")
    trunc_to = max(0, err_byte - args.offset)
    with open(dst, "r+b") as fh:
        fh.truncate(trunc_to)
    log(f"truncated to {trunc_to} bytes (offset {args.offset})")

    rc = run_xmllint_recover(dst)
    if rc == 127:
        log("xmllint not found in PATH; truncated file written without recovery")
    elif rc == 0:
        log(f"xmllint --recover completed; output: {dst}")
    else:
        log(f"xmllint --recover returned {rc}; partial output may still be usable")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
