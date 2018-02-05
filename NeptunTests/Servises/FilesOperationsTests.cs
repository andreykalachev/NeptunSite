using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptun.Servises;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptun.Models.Enum;

namespace Neptun.Servises.Tests
{
    [TestClass()]
    public class FilesOperationsTests
    {
        [TestMethod()]
        public void ChangeFileNameTest()
        {
            //arrange
            var fileName = "Hello.pdf";
            var expectedName = $"Hello{DateTime.Now:yyyyMMddHHmmsstt}.pdf";

            //act
            var newFileName = FilesOperations.ChangeFileName(fileName);

            // assert  
            Assert.AreEqual(expectedName, newFileName);
        }

        [TestMethod()]
        public void IsFilePictureTest()
        {
            //arrange
            const string fileName = "MyImage.png";
            var fileExtention = Path.GetExtension(fileName);

            //act
            var isFilePicture = FilesOperations.IsFilePicture(fileExtention);

            //assert
            Assert.IsTrue(isFilePicture);
        }

        [TestMethod()]
        public void IsFilePictureNoExtensionTest()
        {
            //arrange
            const string fileName = "MyImage";
            var fileExtention = Path.GetExtension(fileName);

            //act
            var isFilePicture = FilesOperations.IsFilePicture(fileExtention);

            //assert
            Assert.IsFalse(isFilePicture);
        }

        [TestMethod()]
        public void IsFilePictureNullTest()
        {
            //arrange
            const string fileName = null;
            var fileExtention = Path.GetExtension(fileName);

            //act
            var isFilePicture = FilesOperations.IsFilePicture(fileExtention);

            //assert
            Assert.IsFalse(isFilePicture);
        }

        [TestMethod()]
        public void IsFilePictureDoubleExtentionTest()
        {
            //arrange
            const string fileName = "Picture.jpg.pdf";
            var fileExtention = Path.GetExtension(fileName);

            //act
            var isFilePicture = FilesOperations.IsFilePicture(fileExtention);

            //assert
            Assert.IsFalse(isFilePicture);
        }
    }
}