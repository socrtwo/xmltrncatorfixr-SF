     ****XML Truncator-Fixer 1.0.0****

In order to deal with the perceived lack of standards with HTML, XML was
designed from the get-go to be intolerant of errors. XML files are used for
many purposes including storing text and data in Microsoft's Office Open XML
file formats. We can see how this XML "precision decision" plays out when
there is one mistake in the document.xml file within the larger docx zip
structure: Word will not display any text. This can be quite frustrating in
a data recovery situation where partial recovery is better than nothing.

XML Truncator-Fixer attempts to fix XML files for these kinds of conditions.
It does so by identifying the first error within a file and then truncating
the file 50 characters before. The extra truncation is sometimes needed
because the XML validator used to find the error is often wrong and indicates
an error only after several characters of bad XML have already been passed.

                 ****What's new in 1.0.0****

The original 2012 release was a Windows-only VB.NET application bundling
xmlval.exe (a packaged Perl XML::Parser), trunc.exe, and xmllint.exe. The
1.0.0 modernization adds:

  * Cross-platform CLI (xmltrncatorfixr.pl / .py) for Linux / macOS /
    ChromeOS / Windows, depending only on libxml2's xmllint.
  * Browser-based Progressive Web App that fixes XML and Office Open XML
    files entirely on-device - installable on Web, Android, iOS, ChromeOS,
    macOS, Linux, and Windows.
  * Updated Windows build now targets .NET Framework 4.8 (the legacy 0.5.x
    binary targeted .NET 2.0).
  * GitHub Actions release workflow produces per-platform zips.

                 ****Credits****

This program was made by Paul Pruitt with VB.Net on Visual Studio Pro 2010
using the .NET 2.0 library; it was modernized in 2026 to .NET Framework 4.8
and gained the cross-platform CLI / PWA. As a validator the original program
used a pp-compiled Perl program that employs the XML::Parser module to
indicate where errors occur. To truncate the XML file XML Truncator-Fixer
uses trunc.exe from the trunc project hosted in Google Apps. Finally, to
add the right ending tags to the truncated XML file, this project uses
xmllint from the Libxml2 project.

                   ****To Do****

1. A loop which removes one character at a time from the error location
   along with the rest of the truncated file and then revalidates the XML
   until it passes, allowing for more complete data recovery.

2. An entire GUI front end to xmllint.

            ****Author Information****

If you have corrupt MS or Open Office files and need help for manual
recovery of text/data, I charge $22 for successful consultations. If you
are unsatisfied with the data recovery, you owe me nothing.

You can send comments, suggestions, or criticism about this program or
corrupt Office files to socrtwo@s2services.com.

--Paul Pruitt
--02/23/2012 (original) / 2026 (modernized)
