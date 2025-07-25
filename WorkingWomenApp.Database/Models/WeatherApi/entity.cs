using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.Models.WeatherApi
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class entity
    {

        private ulong cert_numberField;

        private object old_cert_numberField;

        private string entity_nameField;

        private string company_typeField;

        private string company_subtypeField;

        private string incorporation_dateField;

        private string reg_dateField;

        private string reg_statusField;

        private string ba_categoryField;

        private string ba_divisionField;

        private string ba_groupField;

        private string ba_classField;

        private object diss_dateField;

        private byte compliantField;

        private object incorporated_inField;

        //private entityMetadata metadataField;

        /// <remarks/>
        public ulong cert_number
        {
            get
            {
                return this.cert_numberField;
            }
            set
            {
                this.cert_numberField = value;
            }
        }

        /// <remarks/>
        public object old_cert_number
        {
            get
            {
                return this.old_cert_numberField;
            }
            set
            {
                this.old_cert_numberField = value;
            }
        }

        /// <remarks/>
        public string entity_name
        {
            get
            {
                return this.entity_nameField;
            }
            set
            {
                this.entity_nameField = value;
            }
        }

        /// <remarks/>
        public string company_type
        {
            get
            {
                return this.company_typeField;
            }
            set
            {
                this.company_typeField = value;
            }
        }

        /// <remarks/>
        public string company_subtype
        {
            get
            {
                return this.company_subtypeField;
            }
            set
            {
                this.company_subtypeField = value;
            }
        }

        /// <remarks/>
        public string incorporation_date
        {
            get
            {
                return this.incorporation_dateField;
            }
            set
            {
                this.incorporation_dateField = value;
            }
        }

        /// <remarks/>
        public string reg_date
        {
            get
            {
                return this.reg_dateField;
            }
            set
            {
                this.reg_dateField = value;
            }
        }

        /// <remarks/>
        public string reg_status
        {
            get
            {
                return this.reg_statusField;
            }
            set
            {
                this.reg_statusField = value;
            }
        }

        /// <remarks/>
        public string ba_category
        {
            get
            {
                return this.ba_categoryField;
            }
            set
            {
                this.ba_categoryField = value;
            }
        }

        /// <remarks/>
        public string ba_division
        {
            get
            {
                return this.ba_divisionField;
            }
            set
            {
                this.ba_divisionField = value;
            }
        }

        /// <remarks/>
        public string ba_group
        {
            get
            {
                return this.ba_groupField;
            }
            set
            {
                this.ba_groupField = value;
            }
        }

        /// <remarks/>
        public string ba_class
        {
            get
            {
                return this.ba_classField;
            }
            set
            {
                this.ba_classField = value;
            }
        }

        /// <remarks/>
        public object diss_date
        {
            get
            {
                return this.diss_dateField;
            }
            set
            {
                this.diss_dateField = value;
            }
        }

        /// <remarks/>
        public byte compliant
        {
            get
            {
                return this.compliantField;
            }
            set
            {
                this.compliantField = value;
            }
        }

        /// <remarks/>
        public object incorporated_in
        {
            get
            {
                return this.incorporated_inField;
            }
            set
            {
                this.incorporated_inField = value;
            }
        }

        /// <remarks/>
        //public entityMetadata metadata
        //{
        //    get
        //    {
        //        return this.metadataField;
        //    }
        //    set
        //    {
        //        this.metadataField = value;
        //    }
        //}
    }
}
