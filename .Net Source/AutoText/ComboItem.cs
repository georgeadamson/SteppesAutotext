using System.Windows.Forms;

namespace AutoText
{
    public class ComboItem
    {

        #region Constuctor

        public ComboItem(string comboText, object comboValue)
        {
            this.comboText = comboText;
            this.comboValue = comboValue;
        }

        #endregion

        #region Properties

        public object Value
        {

            get
            {
                return comboValue;
            }
            set
            {
                comboValue = value;
            }
        }

        public string Text
        {
            get
            {
                return comboText;
            }
            set
            {
                comboText = value;
            }
        }

        #endregion

        #region Overriding Methods

        public override string ToString()
        {
            return comboText;
        }

        #endregion

        #region Methods

        public static ComboItem FindByValue(ComboBox combobox, int value)
        {
            foreach (ComboItem item in combobox.Items)  
            {  
                if (item.Value.Equals(value))  
                {  
                    return (ComboItem)item;  
                }  
            }
            return null;  
        } 

        public static void SetComboValue(ComboBox combobox, int value)
        {
            ComboItem comboItem = FindByValue(combobox, value);
            if(comboItem != null)
            {
                combobox.SelectedItem = comboItem;
            }
        }




        #endregion

        #region Fields

        private object comboValue;
        private string comboText;

        #endregion

    }
}
