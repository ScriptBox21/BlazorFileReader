﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace Blazor.FileReader.Demo.Common
{
    public partial class ObjectUrlBase : ComponentBase
    {
        [Inject]
        IFileReaderService FileReaderService { get; set; }

        public ElementReference inputElement;
        public string Output { get; set; }

        public async Task ReadFile()
        {
            Output = string.Empty;
            this.StateHasChanged();
            var files = await FileReaderService.CreateReference(inputElement).EnumerateFilesAsync();

            if (!files.Any())
            {
                await WriteLine("No file selected");
            }

            foreach (var file in files)
            {
                var objectUrl = await file.GetObjectUrlAsync();
                await using (objectUrl)
                {
                    // Print the object url
                    await WriteLine($"Object url: {objectUrl}");
                }

            }
        }

        public async Task WriteLine(string log)
        {
            Output += $"{log}{Environment.NewLine}";
            await InvokeAsync(StateHasChanged);
        }
    }
}
