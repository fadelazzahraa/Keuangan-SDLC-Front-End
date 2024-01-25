using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keuangan
{
    public class Record
    {
		private int id;
		private int actorId;
		private string transaction;
		private float valueRecord;
		private string detail;
		private DateTime date;
		private string tag;
		private int? categoryRecordId;
		private int? photoRecordId;

        public Record(int id, int actorId, string transaction, float valueRecord, string detail, DateTime date, string tag, int? categoryRecordId, int? photoRecordId)
        {
			this.id = id;
			this.actorId = actorId;
			this.transaction = transaction;
			this.valueRecord = valueRecord;
			this.detail = detail;
			this.date = date;
			this.tag = tag;
			this.categoryRecordId = categoryRecordId;
			this.photoRecordId = photoRecordId;
        }

		public int ID
		{
			get { return id; }
			set { id = value; }
		}

        public int ActorId
        {
            get { return actorId; }
            set { actorId = value; }
        }

        public string Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        public float ValueRecord
        {
            get { return valueRecord; }
            set { valueRecord = value; }
        }

        public string Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public int? CategoryRecordId
        {
            get { return categoryRecordId; }
            set { categoryRecordId = value; }
        }

        public int? PhotoRecordId
        {
            get { return photoRecordId; }
            set { photoRecordId = value; }
        }

    }
}
