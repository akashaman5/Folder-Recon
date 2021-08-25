# Folder-Recon
This is a simple C# console app that I wrote for comparing contents of two folders, byte-by-byte.
It does a left to right comparison for each files within both folder.
I initially included the logic to compare based on file size and file name, i.e. if the file size is same then the files are equal. But I removed this logic in later builds due the fact that it would generates false positives.
C# console apps by default don't reset the input fields, so i had to add another logic in later builds to this app that would call the main() method based on certain input.
So after i implemented the point#5 above, it eliminated the need for the user to exit & open the app everytime they want to key in a new input.Thus the reset functionality was added.
I have also done some basic modifications in later builds to make the output messages more readable.


