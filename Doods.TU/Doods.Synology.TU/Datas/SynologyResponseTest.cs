using System;
using Doods.Synology.Webapi.Std;
using Doods.Synology.Webapi.Std.Datas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Synology.TU.Datas
{
    [TestClass]
    public class SynologyResponseTest
    {
        [TestMethod]
        public void Create()
        {
            var v = new SynologyResponse<string>();
            Assert.IsNotNull(v);
            Assert.IsNull(v.Data);
            Assert.IsNull(v.Error);
            Assert.IsNull(v.Errors);
            Assert.IsFalse(v.Success);
            Assert.AreEqual(default, v.Code);
            Assert.AreEqual(default, v.HttpStatus);
        }


        [DataTestMethod]
        [DataRow(default)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        public void SetCode(int code)
        {
            var v = new SynologyResponse<string>();
            v.Code = code;
            Assert.IsNotNull(v);
            Assert.IsNull(v.Data);
            Assert.IsNull(v.Error);
            Assert.IsNull(v.Errors);
            Assert.IsFalse(v.Success);
            Assert.AreEqual(code, v.Code);
            Assert.AreEqual(default, v.HttpStatus);

        }

        [DataTestMethod]
        [DataRow(default)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        public void SetHttpStatus(int httpStatus)
        {
            var v = new SynologyResponse<string>();
            v.HttpStatus = httpStatus;
            Assert.IsNotNull(v);
            Assert.IsNull(v.Data);
            Assert.IsNull(v.Error);
            Assert.IsNull(v.Errors);
            Assert.IsFalse(v.Success);
            Assert.AreEqual(default, v.Code);
            Assert.AreEqual(httpStatus, v.HttpStatus);
           
        }

        [TestMethod]
       
        public void SetData()
        {
            var obj = new CanDo();
            var v = new SynologyResponse<CanDo>();
            v.Data = obj;
            Assert.IsNotNull(v);
            Assert.AreEqual(obj,v.Data);
            Assert.IsNull(v.Error);
            Assert.IsNull(v.Errors);
            Assert.IsFalse(v.Success);
            Assert.AreEqual(default, v.Code);
            Assert.AreEqual(default, v.HttpStatus);

        }

        [DataTestMethod]
        [DataRow(default)]
        [DataRow(false)]
        [DataRow(true)]
        public void SetSuccess(bool success)
        {
            var v = new SynologyResponse<string>();
            v.Success = success;
            Assert.IsNotNull(v);
            Assert.IsNull(v.Data);
            Assert.IsNull(v.Error);
            Assert.IsNull(v.Errors);
            Assert.AreEqual(success,v.Success);
            Assert.AreEqual(default, v.Code);
            Assert.AreEqual(default, v.HttpStatus);

        }
    }
}



//[JsonProperty("error")] public SynologyError Error { get; set; }

//[JsonProperty("errors")] public List<SynologyError> Errors { get; set; }

//[JsonProperty("errormsg")] public dynamic Errormsg { get; set; }
