using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ConvertBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) Console.WriteLine("Merci de donner l'emplacement d'un fichier texte ou binaire à convertir!");
            else 
            {
                string fileName = @args[0];
                string extension = Path.GetExtension(fileName); 
                string directoryName = Path.GetDirectoryName(fileName);
                if (File.Exists(directoryName + "\\result.bin") && !Path.GetFileName(fileName).Equals("result.bin")) 
                {
                    File.Delete(directoryName + "\\result.bin"); //On supprime les anciens résultats
                }
                if (File.Exists(directoryName + "\\result.txt") && !Path.GetFileName(fileName).Equals("result.txt")) 
                {
                    File.Delete(directoryName + "\\result.txt");
                }
                if (extension.Equals(".txt")) //Txt to Bin
                { 
                    try    
                    {      
                        // Relecture du fichier dans la console de commande et transcription dans un nouveau fichier
                        using (StreamReader streamReader = File.OpenText(fileName))    
                        {    
                            string readString = "";    
                            while ((readString = streamReader.ReadLine()) != null)    
                            {    
                                string convertedLine = stringToBinary(readString);  
                                transcriptToBinary(fileName, convertedLine);   
                            }    
                            Console.WriteLine("Conversion complétée!");
                        }      
                    }    
                    //Gestion d'erreurs d'ouverture du fichier
                    catch (Exception exceptionGot) when (exceptionGot.Data != null)    
                    {    
                        Console.WriteLine("Une erreur est survenue. Êtez vous sur que le fichier existe à l'emplacement désigné?");
                            
                    }
                }
                else if (extension.Equals(".bin")) //Bin to Txt
                {  
                    try    
                    {      
                        // Relecture du fichier dans la console de commande, et transcription dans un nouveau fichier
                        using (StreamReader streamReader = File.OpenText(fileName))    
                        {    
                            string readString = "";    
                            while ((readString = streamReader.ReadLine()) != null)    
                            {    
                                string convertedLine = binaryToString(readString);  
                                transcriptToTxt(fileName, convertedLine);   
                            }    
                            Console.WriteLine("Conversion Complétée!");
                        }      
                    }    
                    //Gestion d'erreurs d'ouverture du fichier binaire
                    catch (Exception exceptionGot) when (exceptionGot.Data != null)    
                    {    
                        Console.WriteLine("Une erreur est survenue. Êtez vous sur que le fichier existe à l'emplacement désigné?");
                            
                    }
                }
                else
                {
                    Console.WriteLine("Extension de fichier non prise en compte!");
                }
            } 
        }

        public static string stringToBinary(string data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char character in data.ToCharArray())
            {
                stringBuilder.Append(Convert.ToString(character, 2).PadLeft(8,'0'));
            }
            return stringBuilder.ToString();
        }

        public static string binaryToString(string data)
        {
        List<Byte> byteList = new List<Byte>();
            for (int index = 0; index < data.Length; index += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(index, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static void transcriptToBinary(string fileName, string toTranscript)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            string resultFilePath = directoryName + "\\result.bin";
            if (!File.Exists(resultFilePath))
            {
                try
                {
                    using (FileStream fileStream = File.Create(resultFilePath))     
                    {    
                        Byte[] lineToPrint = new UTF8Encoding(true).GetBytes(toTranscript + "\n");
                        fileStream.Write(lineToPrint, 0, lineToPrint.Length);
                        return;
                    }
                }
                catch (Exception exceptionGot) when (exceptionGot.Data != null)
                {
                    Console.WriteLine("Une erreur d'écriture est survenue!");
                }
            }
            else
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(resultFilePath, true))
                    {
                        outputFile.WriteLine(toTranscript + "\n");
                    }
                }
                catch (Exception exceptionGot) when (exceptionGot.Data != null)
                {
                    Console.WriteLine("Une erreur d'écriture est survenue!");
                }
            }
        }

        public static void transcriptToTxt(string fileName, string toTranscript)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            string resultFilePath = directoryName + "\\result.txt";
            if (!File.Exists(resultFilePath))
            {
                try
                {
                    using (FileStream fileStream = File.Create(resultFilePath))     
                    {    
                        Byte[] lineToPrint = new UTF8Encoding(true).GetBytes(toTranscript + "\n");
                        fileStream.Write(lineToPrint, 0, lineToPrint.Length);
                        return;
                    }
                }
                catch (Exception exceptionGot) when (exceptionGot.Data != null)
                {
                    Console.WriteLine("Une erreur d'écriture est survenue!");
                }
            }
            else
            {
                try
                {
                    using (StreamWriter outputFile = new StreamWriter(resultFilePath, true))
                    {
                        outputFile.WriteLine(toTranscript + "\n");
                    }
                }
                catch (Exception exceptionGot) when (exceptionGot.Data != null)
                {
                    Console.WriteLine("Une erreur d'écriture est survenue!");
                }
            }
        }
    }
}
