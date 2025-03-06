
using System;
using System.IO;
using NUnit.Framework;

namespace DirectoryManager
{
    class DirectoryTest
    {
        [Test]
        public void DirectoryStructureLoading()
        {
            // vytvorenie docasnej zlozky s kratkou strukturou na testovanie
            string tempPath = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(tempPath);
            File.WriteAllText(Path.Combine(tempPath, "rootFile1.txt"), "file1 content");
            string subDir = Path.Combine(tempPath, "SubDir1");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(subDir, "subDirFile1.txt"), "file2 content");

            DirectoryData directoryData = new DirectoryData(tempPath);

            // kontrola ci sa spravne ukladaju subory a pripony suborov
            bool hasFile1 = directoryData.Files.Any(file => file.Name == "rootFile1" && file.Extension == ".txt");
            Assert.That(hasFile1, Is.True);

            //kontrola poctu a nazvov zloziek
            Assert.That(directoryData.Directories.Count, Is.EqualTo(1));
            Assert.That(directoryData.DirectoryName, Is.EqualTo("TestDir"));
            DirectoryData subDirData = directoryData.Directories[0];
            Assert.That(subDirData.DirectoryName, Is.EqualTo("SubDir1"));

            Directory.Delete(tempPath, true);
        }

        [Test]
        public void PrintingDirectoryInfo()
        {
            // vytvorenie docasnej zlozky
            string tempPath = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(tempPath);
            File.WriteAllText(Path.Combine(tempPath, "rootFile1.txt"), "file1 content test");

            var dirData = new DirectoryData(tempPath);
            var printer = new PrintDirectories();

            using (var sw = new StringWriter())
            {
                // vystup z konzoly presmerovany na stingwriter objekt
                Console.SetOut(sw);

                printer.PrintDirectory(dirData);

                string output = sw.ToString().Trim();
                string expected = "TestDir\r\n  rootFile1.txt";

                // kontrola spravnosti formatu, v ktorom je adresar vypisany do konzoly
                Assert.That(output, Is.EqualTo(expected), "The printed format does not match");

                Directory.Delete(tempPath, true);
            }
        }
    }
}