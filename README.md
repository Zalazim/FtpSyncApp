# FtpSyncApp

**FtpSyncApp** is a Windows desktop application designed to synchronize a local directory with a remote server over SFTP (SSH) or FTP.  
It provides fine-grained control over which folders are synchronized, how files are compared, and how remote orphans are handled.

This project is written in C# / .NET using Windows Forms, with file transfers implemented through SSH.NET.

---

## Features

### Folder selection
- Displays the full local directory structure.
- Allows selecting individual folders through checkboxes.
- Parent and child relationships are handled logically:
  - Checking a parent checks all children.
  - Checking a child automatically checks all parents.
  - Unchecked children are not included, even if parents are checked.
- Each folder displays the number of eligible files it contains (taking exclusions into account).

### Synchronization logic
- Upload new files.
- Upload modified files based on:
  - File size comparison
  - Last-write timestamp (two-second tolerance)
- Skip identical files.
- Delete remote orphans when enabled.
- Local scan strictly follows the user-selected folders.

### File exclusion
- Supports exclusion patterns such as `*.ini`, `*.txt`, etc.
- Exclusion rules apply both to local scanning and file-count display.
- Default exclusions can be customized.

### Interface and reporting
- Real-time progress updates.
- Logs grouped into:
  - Information
  - Warnings
  - Errors
- Each log entry includes a timestamp, message, action, status, and file path.
- Logs can be exported in CSV format.

### Safety and error handling
- Dry-run mode (simulation) that performs no remote modifications.
- Warning displayed for files exceeding a configurable size threshold.
- Automatic halt after a configurable number of consecutive failures.
- Full cancellation support.

---

## Technical Architecture

### Local scanning
The application scans only folders that the user has explicitly selected.  
Files are indexed relative to the configured local root for straightforward comparison with remote paths.

### Remote scanning
The remote directory is scanned recursively.  
All paths are normalized using Unix-style separators.  
A dictionary of remote files is produced, keyed by relative path.

### Synchronization planning
A complete plan is generated before execution.  
Each local or remote file receives one of the following actions:

- `UploadNew`
- `UploadUpdate`
- `DeleteRemote`
- `Skip`

### Transfer engine
Transfers are performed through an abstraction layer (`IRemoteClient`).  
The default implementation, `SftpClientWrapper`, uses SSH.NET.  
This design allows future support for additional protocols such as FTP.

---

## Build and Publish

The project targets **.NET 10.0 for Windows** and uses Windows Forms.

### Build
1. Open the solution in Visual Studio.
2. Select the `Release` configuration.
3. Build the project.

### Publish
1. Open the project's Publish profile.
2. Choose **Self-contained** deployment.
3. Select the appropriate runtime (typically `win-x64`).
4. Publish to any directory.

This produces a standalone executable that does not require .NET to be installed on the target environment.

---

## Dependencies

- .NET 10.0
- SSH.NET (NuGet) for SFTP operations

---

## Known Limitations

- FTP support is planned but not implemented yet.
- File comparison uses size and timestamp only (no block-level diff).
- All operations run sequentially.
