﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Penman.EpubSharp.Tests
{
    public class EpubBookTests
    {
        [Fact]
        public void EpubAsPlainTextTest1()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/pg68371.epub"));

            Func<string, string> normalize = text => text.Replace("\r", "").Replace("\n", "").Replace(" ", "");

            var expected = File.ReadAllText(Cwd.Combine(@"Samples/pg68371.txt"));
            var actual = book.ToPlainText();
            Assert.Equal(normalize(expected), normalize(actual));

            var lines = actual.Split('\n').Select(str => str.Trim()).ToList();
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER I."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER II."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER III."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER IV."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER V."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER VI."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER VII."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER VIII."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER IX."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER X."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER XI."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER XII."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER XIII."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER XIV."));
            Assert.NotNull(lines.SingleOrDefault(e => e == "CHAPTER XV."));
        }

        [Fact]
        public void EpubAsPlainTextTest2()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/ios-hackers-handbook.epub"));
            //File.WriteAllText(Cwd.Join("Samples/epub-assorted/iOS Hackers Handbook.txt", book.ToPlainText()));

            Func<string, string> normalize = text => text.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            var expected = File.ReadAllText(Cwd.Combine(@"Samples/ios-hackers-handbook.txt"));
            var actual = book.ToPlainText();

            Assert.Equal(normalize(expected), normalize(actual));
            
            var trimmed = string.Join("\n", actual.Split('\n').Select(str => str.Trim()));
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 1\niOS Security Basics").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 2\niOS in the Enterprise").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 3\nEncryption").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 4\nCode Signing and Memory Protections").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 5\nSandboxing").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 6\nFuzzing iOS Applications").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 7\nExploitation").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 8\nReturn-Oriented Programming").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 9\nKernel Debugging and Exploitation").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 10\nJailbreaking").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "Chapter 11\nBaseband Attacks").Count);
            Assert.Equal(1, Regex.Matches(trimmed, "How This Book Is Organized").Count);
            Assert.Equal(2, Regex.Matches(trimmed, "Appendix: Resources").Count);
            Assert.Equal(2, Regex.Matches(trimmed, "Case Study: Pwn2Own 2010").Count);
        }             
    }
}
