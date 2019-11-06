# README #

Program transforms a series of file paths piped in, one file path per line, into a tree structure, similar to what would Windows' `tree` command create. 

### Intent and purpose: ###

The intended use case for the program is transforming output from hg and git status commands to a tree structure.  
When using git status, option -s for 'short' must be used.  
Paths can be in either Windows-style or UNIX style (backslash '\\' vs slash '/').  
Output paths are in UNIX style (using '/').  
Command line options can be specified either Windows or UNIX style (slash '/' vs dash '-').  

### Switches: ###

	-s 		Input contains status flags from hg/git status commands.\n" +
	-c 		Color the output, which on Windows you must pipe to cmdcolor.exe," +
	   		found here: https://github.com/jeremejevs/cmdcolor\n" +
	-i 		Print tree with 'invisible branches', for 'light' view.\n" +
	-l 		Print a line separator before and after the tree.\n";

### How do I use it? ###

Windows:  
* Unpack the archive and add the containing folder to your system Path.
* Download `cmdcolor` and put it on your Path. Otherwise, remove the `/c` switch and edit the `.bat` file.
* Call it with `PathToTree2.bat`.

Other platforms:
* Rebuild the project from source. Use `dotnet` command, the project is on .NET Core.

### Example: ###
```
> hg status
? asd\gfgfdg\asdasd\asdasd\ghfg.txt
? asd\gfgfdg\asdasd\erert.txt
? asd\gfgfdg\ggdfg.txt
? asd\gfgfdg\ghfh.txt
```
becomes:
```
> hg status | PathToTree2.bat
──────────────────────────────────                   
 asd                                                 
 └─┬ gfgfdg                                          
   │  ? ggdfg.txt                                    
   │  ? ghfh.txt                                     
   └─┬ asdasd                                        
     │  ? erert.txt                                  
     └── asdasd                                      
          ? ghfg.txt                                 
──────────────────────────────────                   
                                   