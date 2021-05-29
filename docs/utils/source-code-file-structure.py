import os

dirsExcludeCompletely = set(["bin", "obj", ".vs", "lib"])
dirsExcludeContents = set(["migrations", "wwwroot"])
filesToExclude = set([
    ".dockerignore",
    "docker-compose.dcproj",
    "Ludo_v2.sln",
    "appsettings.Development.json",
    "appsettings.json",
    "Ludo_API.csproj",
    "Ludo_WebApp.csproj",
    "libman.json",
    "Ludo_WebApp.csproj.user",
    "ScaffoldingReadMe.txt",
    "favicon.ico",
    ]
)

def list_files(startpath):
    for root, dirs, files in os.walk(startpath):
        dirs[:] = [d for d in dirs if d not in dirsExcludeCompletely]
        level = root.replace(startpath, '').count(os.sep)
        indent = ' ' * 4 * (level)
        print('{}- {}/'.format(indent, os.path.basename(root)))
        subindent = ' ' * 4 * (level + 1)
        if os.path.basename(root).lower() in dirsExcludeContents:
            continue
        for f in files:
            if f not in filesToExclude:
                print('{}- {}'.format(subindent, f))

list_files(".\\..\\src")
