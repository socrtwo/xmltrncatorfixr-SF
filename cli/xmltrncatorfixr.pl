#!/usr/bin/env perl
use strict;
use warnings;
use File::Copy;
use File::Basename;
use File::Spec;
use Getopt::Long;

my $VERSION = '1.0.0';

my $offset    = 50;
my $output    = '';
my $in_place  = 0;
my $quiet     = 0;
my $show_help = 0;
my $show_ver  = 0;

GetOptions(
    'offset|n=i' => \$offset,
    'output|o=s' => \$output,
    'in-place|i' => \$in_place,
    'quiet|q'    => \$quiet,
    'help|h'     => \$show_help,
    'version|V'  => \$show_ver,
) or usage(2);

if ($show_help) { usage(0); }
if ($show_ver)  { print "xmltrncatorfixr $VERSION\n"; exit 0; }

my $input = shift @ARGV;
usage(2) unless defined $input && length $input;
die "input file not found: $input\n" unless -f $input;

my $out_path = resolve_output($input, $output, $in_place);
copy($input, $out_path) or die "copy failed: $!\n";

my ($line, $col, $msg) = first_xml_error($out_path);
if (!defined $line) {
    info("no parser error detected; copy written to $out_path");
    exit 0;
}
info("first error at line $line, col $col: $msg");

my $byte = line_col_to_byte($out_path, $line, $col);
my $trunc_to = $byte - $offset;
$trunc_to = 0 if $trunc_to < 0;
truncate_file($out_path, $trunc_to);
info(sprintf("truncated to %d bytes (error byte %d minus offset %d)",
             $trunc_to, $byte, $offset));

my $rc = run_xmllint_recover($out_path);
if ($rc == 0) {
    info("xmllint --recover completed; output: $out_path");
} else {
    info("xmllint --recover returned $rc; partial output may still be usable");
}
exit 0;

sub resolve_output {
    my ($in, $out, $inplace) = @_;
    return $in if $inplace;
    return $out if length $out;
    my ($name, $dir, $ext) = fileparse($in, qr/\.[^.]*/);
    return File::Spec->catfile($dir || '.', "truncated-fixed-$name$ext");
}

sub which {
    my $exe = shift;
    my $sep = ($^O eq 'MSWin32') ? ';' : ':';
    my @ext = ($^O eq 'MSWin32') ? ('', '.exe', '.bat', '.cmd') : ('');
    for my $dir (split /\Q$sep\E/, ($ENV{PATH} || '')) {
        for my $e (@ext) {
            my $p = File::Spec->catfile($dir, $exe . $e);
            return $p if -x $p || ($^O eq 'MSWin32' && -e $p);
        }
    }
    return undef;
}

sub run_capture {
    my @cmd = @_;
    my $pid = open(my $fh, '-|');
    die "fork: $!" unless defined $pid;
    if ($pid == 0) {
        open(STDERR, '>&', \*STDOUT) or die;
        exec(@cmd) or exit 127;
    }
    local $/;
    my $out = <$fh>;
    close $fh;
    return ($? >> 8, defined $out ? $out : '');
}

sub first_xml_error {
    my $path = shift;
    my $xmllint = which('xmllint');
    die "xmllint not found in PATH (install libxml2-utils / libxml2)\n"
        unless defined $xmllint;
    my ($rc, $out) = run_capture($xmllint, '--noout', $path);
    return (undef, undef, undef) if $rc == 0;
    for my $line (split /\r?\n/, $out) {
        if ($line =~ /^\Q$path\E:(\d+):\s*(?:parser\s+error|error)\s*:\s*(.*)$/) {
            return ($1, 1, $2);
        }
        if ($line =~ /:(\d+):(\d+):\s*(.*)$/) {
            return ($1, $2, $3);
        }
    }
    return (undef, undef, $out);
}

sub line_col_to_byte {
    my ($path, $line, $col) = @_;
    open(my $fh, '<:raw', $path) or die "open $path: $!\n";
    my $byte = 0;
    my $cur_line = 1;
    while ($cur_line < $line) {
        my $c;
        my $r = read($fh, $c, 1);
        last unless $r;
        $byte++;
        $cur_line++ if $c eq "\n";
    }
    $byte += ($col > 0 ? $col - 1 : 0);
    close $fh;
    return $byte;
}

sub truncate_file {
    my ($path, $size) = @_;
    open(my $fh, '+<:raw', $path) or die "open $path: $!\n";
    truncate($fh, $size) or die "truncate: $!\n";
    close $fh;
}

sub run_xmllint_recover {
    my $path = shift;
    my $xmllint = which('xmllint');
    return 127 unless defined $xmllint;
    my $tmp = "$path.recover.tmp";
    my ($rc, undef) = run_capture($xmllint, '--recover', '--output', $tmp, $path);
    if (-s $tmp) {
        rename($tmp, $path) or die "rename: $!\n";
    } else {
        unlink $tmp;
    }
    return $rc;
}

sub info { print STDERR "[xmltrncatorfixr] @_\n" unless $quiet; }

sub usage {
    my $code = shift;
    my $fh = $code ? \*STDERR : \*STDOUT;
    print $fh <<'EOF';
Usage: xmltrncatorfixr [options] <input.xml>

Locates the first XML parser error, truncates the file just before the error
(minus a small safety offset), then runs `xmllint --recover` to repair the
closing tags. Useful for partial recovery of corrupt Office Open XML
(docx/xlsx/pptx) document parts.

Options:
  -n, --offset N    bytes to back off before the error (default: 50)
  -o, --output P    write to P (default: truncated-fixed-<input> beside input)
  -i, --in-place    overwrite the input file
  -q, --quiet       suppress progress messages
  -V, --version     print version
  -h, --help        show this help

Requires: xmllint (libxml2-utils on Debian/Ubuntu, libxml2 on macOS/Homebrew,
Windows builds available with libxml2 from xmlsoft.org).
EOF
    exit $code;
}
