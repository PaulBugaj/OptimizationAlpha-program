using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace Communication
{
    public enum FileType { TXT, XLSX };

    class LagrangeInterpolation
    {
        public string FunctionExpression { get; private set; }

        private List<string> functionVariables;

        private List<Vector> vectors;

        public ReadOnlyCollection<Vector> Vectors { get { return this.vectors.AsReadOnly(); } }

        public LagrangeInterpolation()
        {
            this.vectors = new List<Vector>();
            this.FunctionExpression = string.Empty;
            this.functionVariables = new List<string>();
        }


        public bool LoadFile(string path, FileType fileType)
        {
            this.vectors.Clear();
            if (fileType == FileType.TXT)
            {
                try
                {
                    using (TextReader reader = new StreamReader(path))
                    {
                        string line;

                        try
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                //funkcja ktora zamienia na punkty na vektor
                                Vector vector = new Vector();

                                string[] tmp = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                bool ifAllParse = true;

                                for (int i = 0; i < tmp.Length; i++)
                                {
                                    double point_axis;
                                    if (!double.TryParse(tmp[i], out point_axis))
                                    {
                                        ifAllParse = false;
                                    }
                                    else
                                    {
                                        vector.Values.Add(point_axis);
                                    }

                                }

                                if (ifAllParse)
                                {
                                    this.vectors.Add(vector);
                                }

                            }
                        }
                        catch (Exception ex)
                        {

                            if (Debug.DebugState)
                            {
                                Debug.Show(ex.Message);
                            }

                            throw new CommunicationException(CommunicationExceptionType.CannotReadLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Debug.DebugState)
                    {
                        Debug.Show(ex.Message);
                    }
                    throw new CommunicationException(CommunicationExceptionType.CannotLoadFile);
                }
            }
            else if (fileType == FileType.XLSX)
            {
                //staramy sie wczytac plik excela
            }

            return true;
        }

        public bool GenerateFunctionExpression()
        {

            this.FunctionExpression = string.Empty;
            this.functionVariables.Clear();

            if (this.vectors.Count < 1)
            {
                return false;
            }

            string temp_expression = string.Empty;
            double sum_expression = 1.0;

            try
            {
                for (int j = 0; j < this.vectors.Count; j++)
                {
                    for (int i = 0; i < this.vectors.Count; i++)
                    {
                        if (i != j)
                        {
                            if (this.vectors[j].Values[0] < 0)
                            {
                                temp_expression += ("(x+" + (-this.vectors[j].Values[0]).ToString() + ")");
                            }
                            else
                            {
                                temp_expression += ("(x-" + (this.vectors[j].Values[0]).ToString() + ")");
                            }
                            sum_expression = sum_expression * ((this.vectors[i].Values[0]) - (this.vectors[j].Values[0]));
                            temp_expression += (" * ");
                        }
                    }
                    if (this.vectors[j].Values[1] < 0)
                    {
                        temp_expression += ("(" + (this.vectors[j].Values[1]).ToString() + ")" + "/(" + sum_expression + ")");
                    }
                    else
                    {
                        temp_expression += ((this.vectors[j].Values[1]).ToString() + "/(" + sum_expression + ")");
                    }
                    if (sum_expression == 0.0)
                    {
                        temp_expression = ("Devariable x occurs twice - this is not the function!");
                    }

                    if (j < this.vectors.Count - 1)
                    {
                        temp_expression += (" + ");
                    }

                    sum_expression = 1.0;
                }
            }
            catch
            {
                return false;
            }

            this.FunctionExpression = temp_expression.Replace(" ", "");

            return true;
        }
    }
}
