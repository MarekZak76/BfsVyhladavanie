 public class Graph
    {
        public Graph(int x, int y)
        {
            if (x < 0 || y < 0)
                throw new ArgumentOutOfRangeException();

            SizeX = x;
            SizeY = y;
            Matrix = new Node[SizeX, SizeY];

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Matrix[i, j] = new Node();
                }
            }

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Matrix[i, j].Neighbors = new List<Node>
                    {
                        i > 0 ? this.Matrix.GetValue(i-1,j) as Node : null,
                        i > 0 & j < SizeY - 1 ? this.Matrix.GetValue(i-1,j+1) as Node : null,
                        j < SizeY - 1 ? this.Matrix.GetValue(i,j+1) as Node : null,
                        i < SizeX - 1 & j < SizeY - 1 ? this.Matrix.GetValue(i+1,j+1) as Node : null,
                        i < SizeX - 1 ? this.Matrix.GetValue(i+1,j) as Node : null,
                        i < SizeX - 1 & j > 0 ? this.Matrix.GetValue(i+1,j-1) as Node : null,
                        j > 0 ? this.Matrix.GetValue(i,j-1) as Node : null,
                        i > 0 & j > 0 ? this.Matrix.GetValue(i-1,j-1) as Node : null,
                    };
                }
            }
        }

        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public Node[,] Matrix { get; set; }
        public IEnumerable<string> NodesColor
        {
            get
            {
                return new List<string>()
                {
                    "w3-red",
                    "w3-yellow",
                    "w3-purple",
                    "w3-aqua",
                    "w3-blue",
                    "w3-indigo",
                    "w3-green",
                    "w3-teal"
                };
            }
        }

        public async Task<List<List<Node>>> FindSeparateObjects()
        {
            if (SizeX == 0 || SizeY == 0)
                return null;

            Node current;
            Node currentSeparate;
            Queue<Node> queue = new Queue<Node>();
            Queue<Node> queueSeparateObject = new Queue<Node>();
            List<List<Node>> results = new List<List<Node>>();
            IEnumerator<string> colorEnumerator = NodesColor.GetEnumerator();

            // nastavi zaciatok vyhladavania do stredu pola
            int r = this.SizeX / 2;
            int s = this.SizeY / 2;
            queue.Enqueue(Matrix[r, s]);

            while (queue.Any())
            {
                current = queue.Dequeue();

                if (current.Visited)
                    continue;

                current.Visited = true;

                if (current.Data == 0)
                {
                    current.Color = "w3-light-grey";
                }
                if (current.Data == 1 && current.VisitedSeparate == false)
                {
                    queueSeparateObject.Enqueue(current);

                    results.Add(new List<Node>());
                    if (colorEnumerator.MoveNext() == false)
                    {
                        colorEnumerator.Reset();
                        colorEnumerator.MoveNext();
                    }

                    results.Last().Add(current);
                    current.Color = colorEnumerator.Current;
                    await Task.Delay(20); //animacia

                    while (queueSeparateObject.Any())
                    {
                        currentSeparate = queueSeparateObject.Dequeue();

                        currentSeparate.VisitedSeparate = true;

                        foreach (Node item in currentSeparate.Neighbors)
                        {
                            if (item == null || item.Visited || item.VisitedSeparate)
                                continue;

                            if (item.Data == 0)
                            {
                                item.VisitedSeparate = true;
                            }
                            else if (item.Data == 1)
                            {
                                queueSeparateObject.Enqueue(item);
                                results.Last().Add(item);
                                item.VisitedSeparate = true;
                                item.Color = colorEnumerator.Current;
                                await Task.Delay(20);
                            }
                        }
                    }
                }

                foreach (Node item in current.Neighbors)
                    if (item != null && item.Visited == false)
                    {
                        queue.Enqueue(item);
                    }
            }

            return results;
        }
    }
