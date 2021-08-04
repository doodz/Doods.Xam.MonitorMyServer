using System;
using Doods.Openmediavault.Rpc.Std.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Clients
{
    [TestClass]
    public class OMVVersionsUnitTest
    {
        [DataTestMethod]
        [DataRow("NotFound")]
        [DataRow("Ix")]
        [DataRow("Omnius")]
        [DataRow("Fedaykin")]
        [DataRow("Sardaukar")]
        [DataRow("Kralizec")]
        [DataRow("Stone burner")]
        [DataRow("Erasmus")]
        [DataRow("Arrakis")]
        [DataRow("Usul")]
        [DataRow("Shaitan")]
        public void Create(string version)
        {
            var v = OMVVersions.GetVersionFromString(version);
            Assert.IsNotNull(v);
            Assert.AreEqual(version, v.Name);
        }
        [TestMethod]
        public void Create_default()
        {
            var v = new OMVVersion();
            Assert.IsNotNull(v);
            Assert.AreEqual(0, v.Minor);
            Assert.AreEqual(0, v.Major);
            Assert.AreEqual(-1, v.Build);
            Assert.AreEqual(-1, v.Revision);
            
        }
        [TestMethod]
        public void OMVVersion_Parse_NullException()
        {
          

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var v = OMVVersion.Parse(null);
            });
        }
       
        [TestMethod]
        public void NotFound()
        {
            var v = OMVVersions.GetVersionFromString("version");
            Assert.IsNotNull(v);
            Assert.AreEqual("NotFound", v.Name);
        }

        [TestMethod]
        public void Version4lowerThan5()
        {
            var b = (OMVVersions.Version4 < OMVVersions.Version5);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Version4UpperThan2()
        {

            var b = (OMVVersions.Version4 > OMVVersions.Version2);
            Assert.IsTrue(b);
        }


        [TestMethod]
        public void Version4lowerOrEqualThan5()
        {
            var b = (OMVVersions.Version4 <= OMVVersions.Version5);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Version4UpperOREqualThan2()
        {

            var b = (OMVVersions.Version4 >= OMVVersions.Version2);
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void Version4lowerOrEqualThan4()
        {
            var b = (OMVVersions.Version4 <= OMVVersions.Version4);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Version4UpperOREqualThan4()
        {

            var b = (OMVVersions.Version4 >= OMVVersions.Version4);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Versionequal_true()
        {

            var b = (OMVVersions.Version4 == OMVVersions.Version4);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Versionequal_False()
        {

            var b = (OMVVersions.Version4 == OMVVersions.Version5);
            Assert.IsFalse(b);
        }
        [TestMethod]
        public void Version_Clone()
        {

            var clone = (OMVVersion)OMVVersions.Version4.Clone();
           // var b = clone == OMVVersions.Version4;

            Assert.IsNotNull(clone);
        }

        [TestMethod]
        public void Version_Compare4To5()
        {

            var clone = OMVVersions.Version4.CompareTo(OMVVersions.Version5);
            // var b = clone == OMVVersions.Version4;

            Assert.AreEqual(-1, clone);
        }

        [TestMethod]
        public void Version_Compare4To4()
        {

            var clone = OMVVersions.Version4.CompareTo(OMVVersions.Version4);
            // var b = clone == OMVVersions.Version4;

            Assert.AreEqual(0, clone);
        }
        [TestMethod]
        public void Version_CompareTo()
        {

            var clone = OMVVersions.Version4.CompareTo(null);
            // var b = clone == OMVVersions.Version4;

            Assert.AreEqual(1,clone);
        }

        [TestMethod]
        public void Version_CompareTo_2()
        {

            var clone = OMVVersions.Version4.CompareTo((object)null);
            // var b = clone == OMVVersions.Version4;

            Assert.AreEqual(1, clone);
        }

        [TestMethod]
        public void Version_CompareTo_3()
        {

            var clone = OMVVersions.Version4.CompareTo((object)OMVVersions.Version4);
            // var b = clone == OMVVersions.Version4;

            Assert.AreEqual(0, clone);
        }
        [TestMethod]
        public void Version_CompareTo_Exception()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var clone = OMVVersions.Version4.CompareTo("null");
            });
           
        }
        [TestMethod]
        public void VersionNotEqual_true()
        {

            var b = (OMVVersions.Version4 != OMVVersions.Version5);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void VersionNotEqual_False()
        {

            var b = (OMVVersions.Version4 != OMVVersions.Version4);
            Assert.IsFalse(b);
        }

        [DataTestMethod]
        [DataRow(1, 2, 3,4)]
        [DataRow(0, 0, 0,0)]
        [DataRow(int.MaxValue, int.MaxValue, default(int),default(int))]
        public void constructor_4(int major, int minor, int build,int revision)
        {
            var obj = new OMVVersion(major, minor, build, revision);
            Assert.IsNotNull(obj);
            Assert.AreEqual(major, obj.Major);
            Assert.AreEqual(minor, obj.Minor);
            Assert.AreEqual(build, obj.Build);
            Assert.AreEqual(revision , obj.Revision);
        }

        [DataTestMethod]
        [DataRow(1,2,3)]
        [DataRow(0, 0, 0)]
        [DataRow(int.MaxValue, int.MaxValue, default(int))]
        public void constructor_3(int major, int minor, int build)
        {
            var obj = new OMVVersion( major,  minor,  build);
            Assert.IsNotNull(obj);
            Assert.AreEqual(major,obj.Major);
            Assert.AreEqual(minor, obj.Minor);
            Assert.AreEqual(build, obj.Build);
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(0, 0)]
        [DataRow(default(int), int.MaxValue)]
        public void constructor_2(int major, int minor)
        {
            var obj = new OMVVersion(major, minor);
            Assert.IsNotNull(obj);
            Assert.AreEqual(major, obj.Major);
            Assert.AreEqual(minor, obj.Minor);
        }


        [DataTestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(-1, -1)]
        public void constructor_Minor_exception(int major, int minor)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var obj = new OMVVersion(major, minor);
            });
          
           
           
        }

        [DataTestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(-1, -1)]
        public void constructor_Major_exception(int major, int minor)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var obj = new OMVVersion(major, minor);
            });



        }

        [DataTestMethod]
        [DataRow(-1, 0,0)]
        [DataRow(0, -1,0)]
        [DataRow(0, 0, -1)]
        [DataRow(-1, -1,-1)]
        public void constructor_Build_exception(int major, int minor, int build)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var obj = new OMVVersion(major, minor, build);
            });



        }

        [DataTestMethod]
        [DataRow(-1, 0, 0,0)]
        [DataRow(0, -1, 0,0)]
        [DataRow(0, 0, -1,0)]
        [DataRow(0, 0, 0, -1)]
        [DataRow(-1, -1, -1,-1)]
        public void constructor_Revision_exception(int major, int minor, int build,int revision)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var obj = new OMVVersion(major, minor, build, revision);
            });

        }
    }
}