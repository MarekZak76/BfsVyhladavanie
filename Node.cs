 public class Node : INotifyPropertyChanged
    {
        private string color;

        public Node()
        {
            Data = 0;
            Color = "w3-white";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Data { get; set; }
        public string Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }
        public bool Visited { get; set; }
        public bool VisitedSeparate { get; set; }
        public List<Node> Neighbors { get; set; }

        public void ToggleNodeValue()
        {
            if (Color == "w3-white" | Color == "w3-light-grey")
            {
                Color = "w3-black";
                Data = 1;
            }
            else if (Color != "w3-white" & Color != "w3-light-grey")
            {
                Color = "w3-white";
                Data = 0;
            }
        }
       
        public void SetNodeValue()
        {
            Color = "w3-black";
            Data = 1;
        }
        public void ResetNode()
        {
            Visited = false;
            VisitedSeparate = false;
            Color = "w3-white";
            Data = 0;
        }
        public void ClearNode()
        {
            Visited = false;
            VisitedSeparate = false;

            if (Color == "w3-light-grey" || Color == "w3-white")
                Color = "w3-white";
            else Color = "w3-black";
        }

    }
