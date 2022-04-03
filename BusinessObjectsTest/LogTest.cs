using BusinessObjects.Log;
using NUnit.Framework;
using System;
using System.IO;

namespace BusinessObjectsTest
{
    public class LogTest
    {
        private readonly string TestFileLocation = "C:/Temp/Test/";
        private readonly string TestFileName = "TestFileLog";

        [SetUp]
        public void Setup()
        {
            //Clean any previous directories created during testing;
            try
            {
                DirectoryInfo directory = new DirectoryInfo(TestFileLocation);
                if (directory.Exists)
                {
                    directory.Delete(true);
                }
            }catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.Message);
            }

        }

        [Test]
        public void TestLogInit()
        {
            DirectoryInfo directory = new DirectoryInfo(TestFileLocation);
            Assert.IsTrue(!directory.Exists);

            FileLog testLog = new FileLog(TestFileLocation, TestFileName);
            directory = new DirectoryInfo(TestFileLocation);
            Assert.IsTrue(directory.Exists);

            Assert.AreEqual(TestFileLocation, testLog.Path);
            Assert.AreEqual(TestFileName, testLog.Name);
        }

        [Test]
        public void TestLogNewDateFolder()
        {
            DateTime now = DateTime.Now;
            DirectoryInfo directory;

            FileLog testLog = new FileLog(TestFileLocation, TestFileName);
            directory = new DirectoryInfo(TestFileLocation + "/" + now.ToString("yyyy-MM-dd"));
            Assert.IsFalse(directory.Exists);

            testLog.LogInfo("");
            directory = new DirectoryInfo(TestFileLocation + "/" + now.ToString("yyyy-MM-dd"));
            Assert.IsTrue(directory.Exists);
        }

        [Test]
        public void TestLogNewFile()
        {
            DateTime now = DateTime.Now;
            DirectoryInfo directory;

            directory = new DirectoryInfo(TestFileLocation + "/" + now.ToString("yyyy-MM-dd"));
            Assert.IsFalse(File.Exists(directory.FullName + "/" + TestFileName + ".log"));

            FileLog testLog = new FileLog(TestFileLocation, TestFileName);
            testLog.LogInfo(""); 
            Assert.IsTrue(File.Exists(directory.FullName + "/" + TestFileName + ".log"));
        }

        [Test]
        public void TestLogNewFileData()
        {
            DateTime now = DateTime.Now;
            DirectoryInfo directory;


            FileLog testLog = new FileLog(TestFileLocation, TestFileName);
            directory = new DirectoryInfo(TestFileLocation + "\\" + now.ToString("yyyy-MM-dd"));
            directory.Create();
            string fileName = directory.FullName + "\\" + TestFileName + ".log";
            if (File.Exists(fileName)){
                File.Delete(fileName);
            }
            FileStream fileStream = File.Create(fileName);
            Assert.IsTrue(fileStream.Length <= 0);
            fileStream.Close();

            testLog.LogInfo("");

            FileInfo testFileInfo = new FileInfo(fileName);
            Assert.IsTrue(testFileInfo.Length > 0);
        }
    }
}