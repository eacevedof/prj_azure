### info

## List of uers
- curl -X GET  http://localhost:7071/api/users-search?search=eduardoaf.com

## Create User
- curl -X POST -H "Content-Type: application/json" -d '{"fullName":"Eduardo A. F.", "email": "eaf@eaf.com"}' http://localhost:7071/api/user-create

### Ejecución en VSCode
- [Azure Functions in Vscode](https://www.youtube.com/watch?v=mZ6N3gC4KpI&list=PL9Bm8IOGYHA3YmrVUiCKmbg_Nw-Qf26wE&index=2)
- https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cin-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp

- **Carpeta .vscode**
- extensions.json
```json
{
  "recommendations": [
    "ms-azuretools.vscode-azurefunctions",
    "ms-dotnettools.csharp"
  ]
}
```
- Ejecución en modo: **Fn MVC**
- launch.json
```json
{
  // Use IntelliSense to learn about possible attributes.
  // Hover to view descriptions of existing attributes.
  // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Fn MVC",
      "type": "coreclr",
      "request": "attach",
      "processId": "${command:azureFunctions.pickProcess}"
    }
  ]
}
```
- tasks.json
```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "command": "dotnet",
      "type": "process",
      "args": [
        "build",
        "${workspaceFolder}/azurefn_mvc/azurefn_mvc.csproj",
        "--property:GenerateFullPaths=true",
        "--consoleloggerparameters:NoSummary"
      ],
      "problemMatcher": "$msCompile"
    },
    {
      "label": "publish",
      "command": "dotnet",
      "type": "process",
      "args": [
        "publish",
        "${workspaceFolder}/azurefn_mvc/azurefn_mvc.csproj",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "problemMatcher": "$msCompile"
    },
    {
      "label": "watch",
      "command": "dotnet",
      "type": "process",
      "args": [
        "watch",
        "run",
        "--project",
        "${workspaceFolder}/azurefn_mvc/azurefn_mvc.csproj"
      ],
      "problemMatcher": "$msCompile"
    },
    {
      "label": "clean (functions)",
      "command": "dotnet",
      "args": [
        "clean",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "type": "process",
      "problemMatcher": "$msCompile",
      "options": {
        "cwd": "${workspaceFolder}/azurefn_mvc"
      }
    },
    {
      "label": "build (functions)",
      "command": "dotnet",
      "args": [
        "build",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "type": "process",
      "dependsOn": "clean (functions)",
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "problemMatcher": "$msCompile",
      "options": {
        "cwd": "${workspaceFolder}/azurefn_mvc"
      }
    },
    {
      "label": "clean release (functions)",
      "command": "dotnet",
      "args": [
        "clean",
        "--configuration",
        "Release",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "type": "process",
      "problemMatcher": "$msCompile",
      "options": {
        "cwd": "${workspaceFolder}/azurefn_mvc"
      }
    },
    {
      "label": "publish (functions)",
      "command": "dotnet",
      "args": [
        "publish",
        "--configuration",
        "Release",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "type": "process",
      "dependsOn": "clean release (functions)",
      "problemMatcher": "$msCompile",
      "options": {
        "cwd": "${workspaceFolder}/azurefn_mvc"
      }
    },
    {
      "type": "func",
      "dependsOn": "build (functions)",
      "options": {
        "cwd": "${workspaceFolder}/azurefn_mvc/bin/Debug/net6.0"
      },
      "command": "host start",
      "isBackground": true,
      "problemMatcher": "$func-dotnet-watch"
    }
  ]
}
```

## warning
```
vscode: azure function
Failed to verify "AzureWebJobsStorage" connection specified in "local.settings.json". 
Is the local emulator installed and running?
```