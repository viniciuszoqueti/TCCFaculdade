using BaseTCC.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BaseTCC.Utils
{
    public class FileManipulator
    {
        private DirectoryInfo directory;
        private static List<FileInfo> files = null;
        private string subPath = string.Empty;

        public FileManipulator(DirectoryInfo dir)
        {
            this.directory = dir;

            lock (FileManipulator.files)
            {
                FileManipulator.files = dir.GetFiles().ToList();
            }
        }

        public FileManipulator()
        {
            this.directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            this.ReadDirectory();
        }

        public FileManipulator(string subPath)
        {
            this.subPath = subPath;
            this.directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            this.directory.CreateSubdirectory(subPath);
            this.ReadDirectory();
        }

        public FileManipulator(DirectoryInfo dir, string subPath)
        {
            this.directory = dir.CreateSubdirectory(subPath);
            lock (FileManipulator.files)
            {
                FileManipulator.files = dir.GetFiles().ToList();
                this.subPath = subPath;
            }
        }

        public bool ExistsFiles()
        {
            lock (FileManipulator.files)
            {
                if (files.Count() == 0)
                {
                    this.ReadDirectory();
                    return files.Count > 0;
                }
                else
                {
                    return true;
                }
            }
        }

        public void CreateFile(string data, string type)
        {

            StreamWriter wr = null;

            try
            {
                new ValidFileValues(data, type);
            }
            catch (FormatException e)
            {
                throw e;
            }

            try
            {
                string dateFormat = (DateTime.Now.ToString().Replace("/", "").Replace(":", "")).Replace(" ", "");

                StringBuilder builder = new StringBuilder(directory.ToString());
                builder.Append(@"\").Append(this.subPath).Append(@"\");
                builder.Append(dateFormat);
                builder.Append(".").Append(type);

                // string path = Path.Combine(builder.ToString());

                wr = new StreamWriter(builder.ToString(), false);
                wr.WriteLine(data);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                wr.Close();
            }

        }

        public string GetTextFile(string nameFile)
        {
            try
            {
                return this.ReadTextFile(nameFile);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public string GetNameFile(uint fileIndex)
        {

            string nameFile = string.Empty;

            if (files.Count() == 0)
            {
                this.ReadDirectory();
            }

            try
            {
                if (files.Count > 0)
                {

                    FileInfo fileInfo = files[(int)fileIndex];
                    nameFile = fileInfo.Name;

                }

            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return nameFile;
        }

        private string ReadTextFile(string nameFile)
        {
            string text = "";
            StreamReader sr = null;

            if (string.IsNullOrEmpty(nameFile))
            {
                return string.Empty;
            }

            try
            {
                lock (FileManipulator.files)
                {
                    if (files.Count > 0)
                    {

                        StringBuilder builder = new StringBuilder(directory.ToString());
                        builder.Append(@"\").Append(this.subPath);

                        string path = Path.Combine(builder.ToString(), nameFile);

                        if (!this.IsFileLocked(path))
                        {
                            sr = new StreamReader(path);
                            text = sr.ReadToEnd();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                try
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (FileNotFoundException e)
                {
                    throw e;
                }
                catch (DirectoryNotFoundException e)
                {
                    throw e;
                }
                catch (IOException e)
                {
                    throw e;
                }
                catch (NullReferenceException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return text;
        }

        public bool DeleteFile(string nameFile)
        {
            try
            {

                StringBuilder builder = new StringBuilder(directory.ToString());
                builder.Append(@"\").Append(this.subPath);
                builder.Append(@"\").Append(nameFile);

                if (File.Exists(builder.ToString()))
                {
                    FileInfo fileInfoFind = null;

                    lock (FileManipulator.files)
                    {
                        try
                        {
                            fileInfoFind = (from f in files where f.Name == nameFile select f).First();

                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        if (fileInfoFind != null)
                        {
                            try
                            {
                                fileInfoFind.Delete();
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            try
                            {
                                files.Remove(fileInfoFind);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }

                //files[0].Delete();
                //  files.Remove(files.First());
            }
            catch (Exception)
            {
                throw;
            }

            return true;

        }

        private bool IsFileLocked(string file)
        {
            try
            {
                using (var stream = File.OpenRead(file))
                    return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        private void ReadDirectory()
        {
            Thread.Sleep(500);
            StringBuilder builder = new StringBuilder(directory.ToString());
            builder.Append(@"\").Append(this.subPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(builder.ToString());


            if (files != null)
            {
                lock (FileManipulator.files)
                {
                    if (files.Count == 0)
                    {
                        FileManipulator.files.Clear();
                        FileManipulator.files = new List<FileInfo>(directoryInfo.GetFiles().ToList());
                    }
                }
                return;
            }

            FileManipulator.files = new List<FileInfo>(directoryInfo.GetFiles().ToList());
        }

    }
}
