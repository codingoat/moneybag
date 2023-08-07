/*namespace ZUtils
{
    public struct FmodParameter
    {
        public string name;

        private float? value;
        private string valueLabel;

        public float Value
        {
            get => value.Value;
            set
            {
                this.value = value;
                this.valueLabel = null;
            }
        }

        public string ValueLabel
        {
            get => valueLabel;
            set
            {
                this.valueLabel = value;
                this.value = null;
            }
        }

        public bool HasValue => value.HasValue;
        public bool HasValueLabel => valueLabel != null;

        public FmodParameter(string name, float value)
        {
            this.name = name;
            this.value = value;
            this.valueLabel = null;
        }

        public FmodParameter(string name, string valueLabel)
        {
            this.name = name;
            this.value = null;
            this.valueLabel = valueLabel;
        }

        public override string ToString() => $"(Name: {name}, Value: {value}, ValueLabel: {valueLabel})";
    }
}*/