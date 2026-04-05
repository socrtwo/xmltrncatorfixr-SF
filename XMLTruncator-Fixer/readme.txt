     ****XML-Truncator-Fixer 0.5.0.1Beta****

In order to deal with the perceived lack of standards with HTML, XML was designed from get-go to be intolerant of errors. XML files are used for many purposes including storing text and data in Microsoft's new Office Open MS Office file formats. We can see how this XML "precision decision" plays out when there is one mistake in the document.xml file within the larger docx zip structure, Word will not display any text. This can be quite frustrating in a data recovery situation where partial recovery is better than nothing. 

XML Truncator-Fixer attempts to fix XML files for these kinds of conditions. It does so by identifying the first error within a file and then truncating the file 50 characters before. The extra truncation is sometimes needed because the XML validator used to find the error is often wrong and indicates an error only after several characters of bad XML have already been passed. 

                 ****Credits****

This program was made by me, Paul Pruitt with VB.Net on Visual Studio Pro 2010 using the .NET 2.0 library.

As a validator, this program uses a pp compiled Perl program that employs the XML::Parser module to indicate where errors occur. To truncate the XML file XML Truncator-Fixer uses trunc.exe from the trunc project hosted in Google apps. Finally to add the right ending tags to the truncated XML file, this project uses xmllint from the Libxml2 project.

                   ****To Do****

1. Future possibilities for this program are to add a loop which removes one character at a time from the error location along with the rest of the truncated file and then revalidates the XML until it passes, allowing for more complete data recovery.

2. Another possibility is to make an entire GUI front end to XMLLint.

            ****Author Information****

If you have corrupt MS or Open Office files and need help for manual recovery of text/data, I charge $22 for successful consultations. If you are unsatisfied with the data recovery, you owe me nothing.

You can send comments, suggestions or criticism about this program or corrupt Office files to socrtwo@s2services.com.

--Paul Pruitt
--02/23/2012