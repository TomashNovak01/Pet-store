using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pet_store.Resources.Base
{
    public abstract class DocumentBase
    {
        private string _path { get; set; }
        private XWPFDocument _document { get; set; }

        public DocumentBase(string path)
        {
            _path = path;

            using StreamReader stream = new(GetPath());
            _document = new XWPFDocument(stream.BaseStream);
        }

        private string GetPath()
        {
            string currentDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            currentDir = Path.Combine(currentDir, "Resources", "!TemplateOfCheck.docx");

            return currentDir;
        }

        public abstract void BeginDocumentCreation();

        protected List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> GetBookmarks()
        {
            List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> marks = new List<(XWPFParagraph paragraph, CT_Bookmark bookmark)>(1);

            marks.AddRange(GetBookmarksInText());
            marks.AddRange(GetBookmarksInTables());

            return marks;
        }

        protected List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> GetBookmarksInText()
        {
            List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> marks = new List<(XWPFParagraph paragraph, CT_Bookmark bookmark)>(1);

            foreach (XWPFParagraph paragraph in _document.Paragraphs)
            {
                CT_P info = paragraph.GetCTP();

                foreach (CT_Bookmark mark in info.GetBookmarkStartList())
                    marks.Add((paragraph, mark));
            }

            return marks;
        }

        protected List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> GetBookmarksInTables()
        {
            List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> marks = new List<(XWPFParagraph paragraph, CT_Bookmark bookmark)>(1);

            foreach (XWPFTable table in _document.Tables)
                foreach (XWPFTableRow row in table.Rows)
                    foreach (XWPFTableCell cell in row.GetTableCells())
                        foreach (XWPFParagraph paragraph in cell.Paragraphs)
                        {
                            CT_P info = paragraph.GetCTP();

                            foreach (CT_Bookmark bookmark in info.GetBookmarkStartList())
                                marks.Add((paragraph, bookmark));
                        }




            return marks;
        }

        protected void SaveDocument()
        {
            using (Stream stream = new FileStream(_path, FileMode.Create))
                _document.Write(stream);
        }
    }
}
