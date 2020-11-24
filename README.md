# BEFORE YOU DIG IN!

This is an implementation of [Kuriimu2](https://github.com/FanTranslatorsInternational/Kuriimu2) or something similar to it. If you're looking for something robust, well built, and well supported, you should definitely check it out!

# K1TO!

K1TO is an application meant to be the one-stop-shop for all archive files that are compressed or decompressed. In an effort to support multiple different filetypes, we welcome anyone with knowledge to support a specific filetype and add it to our plugins.

# How to use the application

1. Download the application
2. Open the application
3. Click `File` and then `Open` and select your archival format
4. If it is not supported, put in a request

# Goals

K1 is currently planning to

- Have a user interface that is intuitive to use and similar to other archive managers
- Support .zip file extensions
- Support .zip file traversal

## Architecture

The current architecture includes having a singular program state that all other components interface with. This state will be used by the ETO Form to display information to the end user.

The plugin component of this application will have an interface that is easy to use and provide a solution that is both maintainable and extendable. In order to support a new file type a user will only have to fill out a new file that follows the specified information found within the plugin interface.

The compression folder will serve as a library that can be used within the plugins folder. This will support multiple different types of compression as well as decompression.

```
/K1
├── Interfaces
│ ├── ArchiveInterface.cs
│ └──CompressionInterface.cs
├── Compression
│ ├── LZ10 : CompressionInterface
│ ├── Bzip : CompressionInterface
│ └── Huffman: CompressionInterface
├── View
│ └── View.cs
├── ProgramState.cs
└── FileInformation.cs
```
