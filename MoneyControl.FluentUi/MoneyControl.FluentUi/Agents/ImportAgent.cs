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

    //public static async Task<List<ImportFileLineRecord>> LoadImportFile(FluentInputFileEventArgs file, Dictionary<int, string> Files)
    //{
    //    List<ImportFileLineRecord> allFileLines = new();

    //    await file.Buffer.AppendToFileAsync(Files[file.Index]);

    //    using var memoryStream = new MemoryStream(file.Buffer.Data);
    //    using StreamReader reader = new StreamReader(memoryStream);
    //    string line;
    //    while ((line = await reader.ReadLineAsync()) != null)
    //    {
    //        try
    //        {

    //        List<string> data = line.Split(',').ToList();
    //        if (data[0].ToUpperInvariant() == "DATE") continue;
    //        if (data.Count >= 4)
    //        {
    //                allFileLines.Add(    
    //                    new ImportFileLineRecord(line,
    //                                    data,
    //                                    data[0] ?? string.Empty,
    //                                    data[1] ?? string.Empty,
    //                                    data[2] ?? string.Empty,
    //                                    data[3] ?? string.Empty)
    //                    );
    //        }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
            
    //    }

    //    return allFileLines;
    //}

    public static async Task<List<ImportFileLineRecord>> LoadImportFile( int accountId, FluentInputFileEventArgs file, Dictionary<int, string> Files)
    {
        List<ImportFileLineRecord> allFileLines = new();

        await file.Buffer.AppendToFileAsync(Files[file.Index]);

        using var memoryStream = new MemoryStream(file.Buffer.Data);
        using StreamReader reader = new StreamReader(memoryStream);
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            try
            {
                List<string> data = line.Split(',').ToList();
                data = data.Select(s => s.Replace("\"", "")).ToList();

                string dataZero = data[0].ToUpperInvariant();
                if (dataZero == "DATE" || dataZero == "Description".ToUpperInvariant()) continue;
                int dataCount = data.Count;
                if (accountId == 1)
                {
                    if (dataCount >= 4)
                    {
                        allFileLines.Add(
                            new ImportFileLineRecord(line,
                                            data,
                                            data[0] ?? string.Empty,
                                            data[1] ?? string.Empty,
                                            data[2] ?? string.Empty,
                                            data[3] ?? string.Empty)
                            );
                    }
                }
                else if (accountId == 2) 
                {
                    if (dataCount >= 5)
                    {
                        string fundsIn = string.Empty;
                        string fundsOut = string.Empty;
                        Decimal.TryParse(data[5], out decimal amountValue);
                        if (amountValue > 0) { fundsIn = amountValue.ToString(); }
                        else { fundsOut = amountValue.ToString(); }
                        allFileLines.Add(
                            new ImportFileLineRecord(line,
                                            data,
                                            data[3] ?? string.Empty,
                                            data[0] ?? string.Empty,
                                            fundsOut,
                                            fundsIn)
                            );
                    }
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
