using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text;

namespace MoneyControl.FluentUi.Agents;

public static class ImportAgent
{
    public static async Task<List<string>> LoadImportFileSimplii( FluentInputFileEventArgs file)
    {
        List<string> allLines = new();

        var fileInfo = file.LocalFile;
        
        using StreamReader reader = fileInfo.OpenText();
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            allLines.Add(line);
        }
        return allLines;
    }

    private static async Task<List<String>> ReadFile(IBrowserFile entryFile, string endOfLine)
    {
        var ms = new MemoryStream();
        await entryFile.OpenReadStream().CopyToAsync(ms);
        string fileString = Encoding.UTF8.GetString(ms.ToArray());

        // List<string> fileLinesSlashN = fileString.Split( "\n" ).ToList();
        List<string> fileLines = fileString.Split(endOfLine).ToList();
        return fileLines;
    }

    public static async Task<List<FileLine>> LoadImportFile(FluentInputFileEventArgs file, Dictionary<int, string> Files)
    {
        List<FileLine> allFileLines = new();

        await file.Buffer.AppendToFileAsync(Files[file.Index]);

        using var memoryStream = new MemoryStream(file.Buffer.Data);
        using StreamReader reader = new StreamReader(memoryStream);
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            try
            {

            List<string> data = line.Split(',').ToList();
            if (data[0].ToUpperInvariant() == "DATE") continue;
            if (data.Count >= 4)
            {
                    Decimal.TryParse(data[2], out decimal fundsOut);
                    Decimal.TryParse(data[2], out decimal fundsIn);
                    allFileLines.Add(    
                        new FileLine(line,
                                        data,
                                        data[0] ?? string.Empty,
                                        data[1] ?? string.Empty,
                                        data[2] ?? string.Empty,
                                        data[3] ?? string.Empty)
                        );
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        return allFileLines;
    }

    
    
}
