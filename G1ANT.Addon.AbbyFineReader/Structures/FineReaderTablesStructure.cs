using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FREngine;
using System.Collections;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    public class FineReaderTables : IReadOnlyList<FineReaderTable>
    {
        private readonly FRDocument document;
        private IList<FineReaderTable> tables = null;

        internal FineReaderTables(FRDocument document)
        {
            this.document = document;
        }

        public FineReaderTable this[int index]
        {
            get
            {
                if (tables == null)
                    InitTables();
                return tables[index];
            }
        }

        public int Count
        {
            get
            {
                if (tables == null)
                    InitTables();
                return tables.Count;
            }
        }

        public IEnumerator<FineReaderTable> GetEnumerator()
        {
            if (tables == null)
                InitTables();
            return tables.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (tables == null)
                InitTables();
            return ((System.Collections.IEnumerable)tables).GetEnumerator();
        }

        private void InitTables()
        {
            tables = new List<FineReaderTable>();

            foreach (FRPage page in document.Pages)
                foreach (Block b in page.Layout.Blocks)
                    if (b.Type == BlockTypeEnum.BT_Table)
                        tables.Add(new FineReaderTable(b.GetAsTableBlock()));
        }
    }
}
