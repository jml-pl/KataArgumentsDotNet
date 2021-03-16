using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Arguments;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testCreateWithNoSchemaOrArguments()
        {
            Args args = new Args("", new String[0]);
        }

        [TestMethod]
        public void testWithNoSchemaButWithOneArgument() {
            try
            {
                new Args("", new String[] { "-x" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testWithNoSchemaButWithMultipleArguments() {
            try {
                new Args("", new String[] { "-x", "-y" });
                Assert.Fail();
            } 
            catch (ArgsException e) {
                Assert.AreEqual(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testNonLetterSchema()
        {
            try
            {
                new Args("*", new String[] { });
                Assert.Fail("Args constructor should have thrown exception");
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, e.errorCode);
                Assert.AreEqual('*', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testInvalidArgumentFormat()
        {
            try
            {
                new Args("f~", new String[] { });
                Assert.Fail("Args constructor should have throws exception");
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.INVALID_ARGUMENT_FORMAT, e.errorCode);
                Assert.AreEqual('f', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testSimpleBooleanPresent()
        {
            Args args = new Args("x", new String[] { "-x" });
            Assert.AreEqual(true, (bool) args.getValue('x'));
            Assert.AreEqual(1, args.currentArgument);
        }

        [TestMethod]
        public void testSimpleStringPresent()
        {
            Args args = new Args("x*", new String[] { "-x", "param" });
            Assert.IsTrue(args.has('x'));
            Assert.AreEqual("param", args.getString('x'));
            Assert.AreEqual(2, args.currentArgument);
        }

        [TestMethod]
        public void testMissingStringArgument()
        {
            try
            {
                new Args("x*", new String[] { "-x" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.MISSING_STRING, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testSpacesInFormat()
        {
            Args args = new Args("x, y", new String[] { "-xy" });
            Assert.IsTrue(args.has('x'));
            Assert.IsTrue(args.has('y'));
            Assert.AreEqual(1, args.currentArgument);
        }

        [TestMethod]
        public void testSimpleIntPresent()
        {
            Args args = new Args("x#", new String[] { "-x", "42" });
            Assert.IsTrue(args.has('x'));
            Assert.AreEqual(42, args.getInt('x'));
            Assert.AreEqual(2, args.currentArgument);
        }

        [TestMethod]
        public void testInvalidInteger()
        {
            try
            {
                new Args("x#", new String[] { "-x", "Forty two" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.INVALID_INTEGER, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
                Assert.AreEqual("Forty two", e.errorParameter);
            }

        }

        [TestMethod]
        public void testMissingInteger()
        {
            try
            {
                new Args("x#", new String[] { "-x" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.MISSING_INTEGER, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testSimpleDoublePresent()
        {
            Args args = new Args("x##", new String[] { "-x", "42.3" });
            Assert.IsTrue(args.has('x'));
            Assert.AreEqual(42.3, args.getDouble('x'), .001);
        }

        [TestMethod]
        public void testInvalidDouble()
        {
            try
            {
                new Args("x##", new String[] { "-x", "Forty two" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.INVALID_DOUBLE, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
                Assert.AreEqual("Forty two", e.errorParameter);
            }
        }

        [TestMethod]
        public void testMissingDouble()
        {
            try
            {
                new Args("x##", new String[] { "-x" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.MISSING_DOUBLE, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void testStringArray()
        {
            Args args = new Args("x[*]", new String[] { "-x", "alpha" });
            Assert.IsTrue(args.has('x'));
            String[] result = args.getStringArray('x');
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("alpha", result[0]);
        }

        [TestMethod]
        public void testMissingStringArrayElement()
        {
            try
            {
                new Args("x[*]", new String[] { "-x" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.MISSING_STRING, e.errorCode);
                Assert.AreEqual('x', e.errorArgumentId);
            }
        }

        [TestMethod]
        public void manyStringArrayElements()
        {
            Args args = new Args("x[*]", new String[] { "-x", "alpha", "-x", "beta", "-x", "gamma" });
            Assert.IsTrue(args.has('x'));
            String[] result = args.getStringArray('x');
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("alpha", result[0]);
            Assert.AreEqual("beta", result[1]);
            Assert.AreEqual("gamma", result[2]);
        }

        [TestMethod]
        public void MapArgument()
        {
            Args args = new Args("f&", new String[] { "-f", "key1:val1,key2:val2" });
            Assert.IsTrue(args.has('f'));
            Dictionary<String, String> dict = args.getMap('f');
            Assert.AreEqual("val1", dict["key1"]);
            Assert.AreEqual("val2", dict["key2"]);
        }

        [TestMethod]
        public void malFormedMapArgument()
        {
            try
            {
                new Args("f&", new String[] { "-f", "key1:val1,key2" });
                Assert.Fail();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ArgsException.ErrorCode.MALFORMED_MAP, e.errorCode);
                Assert.AreEqual('f', e.errorArgumentId);
                Assert.AreEqual("key1:val1,key2", e.errorParameter);
            }

            
        }

        [TestMethod]
        public void oneMapArgument()
        {
            Args args = new Args("f&", new String[] { "-f", "key1:val1" });
            Assert.IsTrue(args.has('f'));
            Dictionary<String, String> dict = args.getMap('f');
            Assert.AreEqual("val1", dict["key1"]);
        }

        [TestMethod]
        public void testExtraArguments()
        {
            Args args = new Args("x,y*", new String[] { "-x", "-y", "alpha", "beta" });
            Assert.IsTrue(args.getBool('x'));
            Assert.AreEqual("alpha", args.getString('y'));
            Assert.AreEqual(3, args.currentArgument);
        }

        [TestMethod]
        public void testExtraArgumentsThatLookLikeFlags()
        {
            Args args = new Args("x,y", new String[] { "-x", "alpha", "-y", "beta" });
            Assert.IsTrue(args.has('x'));
            Assert.IsFalse(args.has('y'));
            Assert.IsTrue(args.getBool('x'));
            Assert.IsFalse(args.getBool('y'));
            Assert.AreEqual(1, args.currentArgument);
        }





    }
}
