#region Using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    internal class Store<TEntity> : List<TEntity>
    {
        private int RecordCount;

        public Store()
        {
            RecordCount = 0;           
        }

        public IEnumerable<TEntity> GetNextPage(int batchSize)
        {
            var page = this.Skip(RecordCount).Take(batchSize).ToArray();

            RecordCount += Math.Min(batchSize, page.Length);

            return page;
        }   
        
        public new void Clear()
        {
            base.Clear();

            RecordCount = 0;
        }     
    }
}
