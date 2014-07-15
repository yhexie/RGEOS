using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RGeos.Geometries
{
    public class Matrix
    {
        private int numColumns = 0;
        private int numRows = 0;
        private double eps = 0.0;
        private double[] elements = null;  //矩阵数据缓冲区

        //属性：矩阵列数
        public int Columns
        {
            get
            {
                return numColumns;
            }
        }

        //属性：矩阵行数
        public int Rows
        {
            get
            {
                return numRows;
            }
        }

        //索引器：访问矩阵元素
        public double this[int row, int col]
        {
            get
            {
                return elements[col + row * numColumns];
            }
            set
            {
                elements[col + row * numColumns] = value;
            }
        }

        //属性：Eps
        public double Eps
        {
            get
            {
                return eps;
            }
            set
            {
                eps = value;
            }
        }

        //基本构造函数
        public Matrix()
        {
            numColumns = 1;
            numRows = 1;
            Init(numRows, numColumns);
        }

        //指定行列构造函数
        public Matrix(int nRows, int nCols)
        {
            numRows = nRows;
            numColumns = nCols;
            Init(numRows, numColumns);
        }

        /*指定值构造函数
         nRows指定的矩阵列数
         nCols指定的矩阵行数
         Value一维数组，长度为nRows*nCols，存储矩阵各元素的值
         */
        public Matrix(int nRows, int nCols, double[] value)
        {
            numRows = nRows;
            numColumns = nCols;
            Init(numRows, numColumns);
            SetData(value);
        }

        //方阵构造函数  nSize方阵行列数
        public Matrix(int nSize)
        {
            numRows = nSize;
            numColumns = nSize;
            Init(nSize, nSize);
        }

        /*方阵构造函数
         nSize方阵行列数
         value一维数组，长度为nRows*nRows，存储方阵各元素的值
         */
        public Matrix(int nSize, double[] value)
        {
            numRows = nSize;
            numColumns = nSize;
            Init(nSize, nSize);
            SetData(value);
        }

        //拷贝构造函数 other源矩阵
        public Matrix(Matrix other)
        {
            numColumns = other.GetNumColumns();
            numRows = other.GetNumRows();
            Init(numRows, numColumns);
            SetData(other.elements);
        }

        /*初始化函数
         nRows指定的矩阵行数
         nCols指定的矩阵列数
         return bool，成功返回true，否则返回false
         */
        public bool Init(int nRows, int nCols)
        {
            numRows = nRows;
            numColumns = nCols;
            int nSize = nCols * nRows;
            if (nSize < 0)
                return false;

            //分配内存
            elements = new double[nSize];
            return true;
        }

        //设置矩阵运算精度 newEps新的精度值
        public void SetEps(double newEps)
        {
            eps = newEps;
        }

        /*重载+运算符
         * 
         * return Matrix对象
         */
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return m1.Add(m2);
        }

        /*重载-运算符
         * 
         * return Matrix对象
         */
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1.Subtract(m2);
        }

        /* 重载*运算符
         * 
         * return Matrix对象
         */
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return m1.Multiply(m2);
        }

        /*重载double[]运算符
         * 
         * returndouble[]对象
         */
        public static implicit operator double[](Matrix m)
        {
            return m.elements;
        }

        /*
         * 将方阵初始化为单位矩阵
         * nSize方阵行列数
         * returnbool型，初始化是否成功
         */
        public bool MakeUnitMatrix(int nSize)
        {
            if (!Init(nSize, nSize))
                return false;

            for (int i = 0; i < nSize; ++i)
                for (int j = 0; j < nSize; ++j)
                    if (i == j)
                        SetElement(i, j, 1);

            return true;
        }

        /*
         * 将矩阵各元素的值转化为字符串，元素之间的分隔符为“，”，行与行之间有回车换行符
         * returnstring型，转换得到的字符串
         */
        public override string ToString()
        {
            return ToString(",", true);
        }

        /*
         * 将矩阵各元素的值转化为字符串
         * sDelim元素之间的分隔符
         * bLineBreak 行与行之间是否有回车换行符
         * return string型，转换得到的字符串
         */
        public string ToString(string sDelim, bool bLineBreak)
        {
            string s = "";
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    string ss = GetElement(i, j).ToString("F");
                    s += ss;
                    if (bLineBreak)
                    {
                        if (j != numColumns - 1)
                            s += sDelim;
                    }
                    else
                    {
                        if (i != numRows - 1 || j != numColumns - 1)
                            s += sDelim;
                    }
                }
                if (bLineBreak)
                    if (i != numRows - 1)
                        s += "\r\n";
            }
            return s;
        }

        /*
         * 将矩阵指定行中各元素的值转化为字符串
         * nRow指定的矩阵行，nRow=0表示第一行
         * sDelim元素之间的分隔符
         * returnstring型，转换得到的字符串
         */
        public string ToStringRow(int nRow, string sDelim)
        {
            string s = "";
            if (nRow >= numRows)
                return s;
            for (int j = 0; j < numColumns; ++j)
            {
                string ss = GetElement(nRow, j).ToString("F");
                s += ss;
                if (j != numColumns - 1)
                    s += sDelim;
            }
            return s;
        }

        /*
         * 将矩阵指定列中各元素的值转化为字符串
         * nCol指定的矩阵列，nCol=0表示第一列
         * sDelim元素之间的分隔符
         * returnstring型，转换得到的字符串
         */
        public string ToStringCol(int nCol, string sDelim)
        {
            string s = "";
            if (nCol >= numColumns)
                return s;

            for (int i = 0; i < numRows; ++i)
            {
                string ss = GetElement(i, nCol).ToString("F");
                s += ss;
                if (i != numRows - 1)
                    s += sDelim;
            }
            return s;
        }

        /*
         * 设置矩阵各元素的值
         * value 一维数组，长度为numColumns*numRows，存储矩阵各元素的值
         */
        public void SetData(double[] value)
        {
            elements = (double[])value.Clone();
        }

        /*
         * 设置指定元素的值
         * nRow元素的行
         * nCol元素的列
         * value 指定元素的值
         * return bool型，说明设置是否成功
         */
        public bool SetElement(int nRow, int nCol, double value)
        {
            if (nCol < 0 || nCol >= numColumns || nRow < 0 || nRow >= numRows)
                return false;                       //array bounds error

            elements[nCol + nRow * numColumns] = value;
            return true;
        }

        /*
         * 获取指定元素的值
         * nRow元素的行
         * nCol元素的列
         * return double型，指定元素的值
         */
        public double GetElement(int nRow, int nCol)
        {
            return elements[nCol + nRow * numColumns];
        }

        /*
         * 获取矩阵的列数
         * return int型，矩阵的列数
         */
        public int GetNumColumns()
        {
            return numColumns;
        }

        /*
         * 获取矩阵的行数
         * return int 型，矩阵的行数
         */
        public int GetNumRows()
        {
            return numRows;
        }

        /*
  * 获取矩阵的数据
  * return double 型数组，指向矩阵各元素的数据缓冲区
  */
        public double[] GetData()
        {
            return elements;
        }

        /*
         * 获取指定行的向量
         * nRow向量所在的行
         * pVector指向向量中各元素的缓冲区
         * return int型，向量中元素的个数，即矩阵的列数
         */
        public int GetRowVector(int nRow, double[] pVector)
        {
            for (int j = 0; j < numColumns; ++j)
                pVector[j] = GetElement(nRow, j);
            return numColumns;
        }

        /*
         * 获取指定列的向量
         * nCol向量所在的列
         * pVector指向向量中各元素的缓冲区
         * return int型，向量中元素的个数，即矩阵的行数
         */
        public int GetColVector(int nCol, double[] pVector)
        {
            for (int i = 0; i < numRows; ++i)
                pVector[i] = GetElement(i, nCol);
            return numRows;
        }

        /*
         * 给矩阵赋值
         * other 用于给矩阵赋值的源矩阵
         * retur Matrix型，矩阵与other相等
         */
        public Matrix SetValue(Matrix other)
        {
            if (other != this)
            {
                Init(other.GetNumRows(), other.GetNumColumns());
                SetData(other.elements);
            }
            //finally return a reference to ourselves
            return this;
        }

        /*
         * 判断矩阵是否相等
         * other 用于比较的矩阵
         * return bool型，两个矩阵相等则为true，否则为false
         */
        public override bool Equals(object other)
        {
            Matrix matrix = other as Matrix;
            if (matrix == null)
                return false;
            //首先检查行列数是否相等
            if (numColumns != matrix.GetNumColumns() || numRows != matrix.GetNumRows())
                return false;

            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    if (Math.Abs(GetElement(i, j) - matrix.GetElement(i, j)) > eps)
                        return false;
                }
            }
            return true;
        }

        /*
         * 因为重写了Equals，因此必须重写GetHashCode
         * return int型，返回复数对象散列码
         */
        public override int GetHashCode()
        {
            double sum = 0;
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    sum += Math.Abs(GetElement(i, j));
                }
            }
            return (int)Math.Sqrt(sum);
        }

        /*
         * 实现矩阵的加法
         * other与指定矩阵相加的矩阵
         * return Matrix型，指定矩阵与other相加之和
         * 如果矩阵的行/列数不匹配，则会抛出异常
         */
        public Matrix Add(Matrix other)
        {
            //首先检查行列数是否相等
            if (numColumns != other.GetNumColumns() ||
                numRows != other.GetNumRows())
                throw new Exception("矩阵的行/列数不匹配。");

            //构造结果矩阵
            Matrix result = new Matrix(this);      //拷贝构造

            //矩阵加法
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                    result.SetElement(i, j, result.GetElement(i, j) +
                        other.GetElement(i, j));
            }
            return result;
        }

        /*
         * 实现矩阵的减法
         * other与指定矩阵相减的矩阵
         * return Matrix型，指定矩阵与other相减之差
         * 如果矩阵的行/列数不匹配，则会抛出异常
         */
        public Matrix Subtract(Matrix other)
        {
            if (numColumns != other.GetNumColumns() ||
                numRows != other.GetNumRows())
                throw new Exception("矩阵的行/列数不匹配。");

            //构造结果矩阵
            Matrix result = new Matrix(this);     //拷贝构造

            //进行减法操作
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                    result.SetElement(i, j, result.GetElement(i, j) -
                        other.GetElement(i, j));
            }
            return result;
        }

        /*
         * 实现矩阵的数乘
         * value与指定矩阵相乘的实数
         * return Matrix型，指定矩阵与value相乘之积
         */
        public Matrix Multiply(double value)
        {
            //构造目标矩阵
            Matrix result = new Matrix(this);     //copy ourselves

            //进行数乘
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                    result.SetElement(i, j, result.GetElement(i, j) * value);
            }
            return result;
        }

        /*
         * 实现矩阵的乘法
         * other与指定矩阵相乘的矩阵
         * return Matrix型，指定矩阵与other相乘之积
         * 如果矩阵的行/列数不匹配，则会抛出异常
         */
        public Matrix Multiply(Matrix other)
        {
            //首先检查行列数是否符合要求
            if (numColumns != other.GetNumRows())
                throw new Exception("矩阵的行/列数不匹配。");

            //ruct the object we are going to return
            Matrix result = new Matrix(numRows, other.GetNumColumns());

            //矩阵乘法，即
            //
            //
            //
            double value;
            for (int i = 0; i < result.GetNumRows(); ++i)
            {
                for (int j = 0; j < other.GetNumColumns(); ++j)
                {
                    value = 0.0;
                    for (int k = 0; k < numColumns; ++k)
                    {
                        value += GetElement(i, k) * other.GetElement(k, j);
                    }
                    result.SetElement(i, j, value);
                }
            }
            return result;
        }

        /*
         * 复矩阵的乘法
         * AR左边复矩阵的实部矩阵
         * AI左边复矩阵的虚部矩阵
         * BR右边复矩阵的实部矩阵
         * BI右边复矩阵的虚部矩阵
         * CR乘积复矩阵的实部矩阵
         * CI乘积复矩阵的虚部矩阵
         * return bool型，复矩阵乘法是否成功
         */
        public bool Multiply(Matrix AR, Matrix AI, Matrix BR, Matrix BI, Matrix CR, Matrix CI)
        {
            //首先检查行列数是否符合要求
            if (AR.GetNumColumns() != AI.GetNumColumns() ||
                AR.GetNumRows() != AI.GetNumRows() ||
                BR.GetNumColumns() != BI.GetNumColumns() ||
                BR.GetNumRows() != BI.GetNumRows() ||
                AR.GetNumColumns() != BR.GetNumRows())
                return false;

            //构造乘积矩阵实部矩阵和虚部矩阵
            Matrix mtxCR = new Matrix(AR.GetNumRows(), BR.GetNumColumns());
            Matrix mtxCI = new Matrix(AR.GetNumRows(), BR.GetNumColumns());

            //复矩阵相乘
            for (int i = 0; i < AR.GetNumRows(); ++i)
            {
                for (int j = 0; j < BR.GetNumColumns(); ++j)
                {
                    double vr = 0;
                    double vi = 0;
                    for (int k = 0; k < AR.GetNumColumns(); ++k)
                    {
                        double p = AR.GetElement(i, k) * BR.GetElement(k, j);
                        double q = AI.GetElement(i, k) * BI.GetElement(k, j);
                        double s = (AR.GetElement(i, k) +
                            AI.GetElement(i, k)) * (BR.GetElement(k, j) +
                            BI.GetElement(k, j));
                        vr += p - q;
                        vi += s - p - q;
                    }
                    mtxCR.SetElement(i, j, vr);
                    mtxCI.SetElement(i, j, vi);
                }
            }
            CR = mtxCR;
            CI = mtxCI;

            return true;

        }

        /*
         * 矩阵的转置
         * return Matrix型，指定矩阵转置矩阵
         */
        public Matrix Transpose()
        {
            //构造目标矩阵
            Matrix Trans = new Matrix(numColumns, numRows);

            //转置各元素
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                    Trans.SetElement(j, i, GetElement(i, j));
            }
            return Trans;
        }

        /*实矩阵求逆的全选主元高斯-约当法
         * return bool型，求逆是否成功
         */
        public bool InvertGaussJordan()
        {
            int i, j, k, l, u, v;
            double d = 0, p = 0;

            //分配内存
            int[] pnRow = new int[numColumns];
            int[] pnCol = new int[numColumns];

            //消元
            for (k = 0; k <= numColumns - 1; k++)
            {
                d = 0.0;
                for (i = k; i <= numColumns - 1; i++)
                {
                    for (j = k; j <= numColumns - 1; j++)
                    {
                        l = i * numColumns + j;
                        p = Math.Abs(elements[l]);
                        if (p > d)
                        {
                            d = p;
                            pnRow[k] = i;
                            pnCol[k] = j;
                        }
                    }
                }

                //失败
                if (d == 0.0)
                {
                    return false;
                }

                if (pnRow[k] != k)
                {
                    for (j = 0; j <= numColumns - 1; j++)
                    {
                        u = k * numColumns + j;
                        v = pnRow[k] * numColumns + j;
                        p = elements[u];
                        elements[u] = elements[v];
                        elements[v] = p;
                    }
                }
                if (pnCol[k] != k)
                {
                    for (i = 0; i <= numColumns - 1; i++)
                    {
                        u = i * numColumns + k;
                        v = i * numColumns + pnCol[k];
                        p = elements[u];
                        elements[u] = elements[v];
                        elements[v] = p;
                    }
                }

                l = k * numColumns + k;
                elements[l] = 1.0 / elements[l];
                for (j = 0; j < numColumns - 1; j++)
                {
                    if (j != k)
                    {
                        u = k * numColumns + j;
                        elements[u] = elements[u] * elements[l];
                    }
                }

                for (i = 0; i <= numColumns - 1; i++)
                {
                    if (i != k)
                    {
                        for (j = 0; j <= numColumns - 1; j++)
                        {
                            if (j != k)
                            {
                                u = i * numColumns + j;
                                elements[u] = elements[u] -
                                    elements[i * numColumns + k] *
                                    elements[k * numColumns + j];
                            }
                        }
                    }
                }

                for (i = 0; i <= numColumns - 1; i++)
                {
                    if (i != k)
                    {
                        u = i * numColumns + k;
                        elements[u] = -elements[u] * elements[l];
                    }
                }
            }

            //调整恢复行列次序
            for (k = numColumns - 1; k >= 0; k--)
            {
                if (pnCol[k] != k)
                {
                    for (j = 0; j <= numColumns - 1; j++)
                    {
                        u = k * numColumns + j;
                        v = pnCol[k] * numColumns + j;
                        p = elements[u];
                        elements[u] = elements[v];
                        elements[v] = p;
                    }
                }
                if (pnRow[k] != k)
                {
                    for (i = 0; i <= numColumns - 1; i++)
                    {
                        u = i * numColumns + k;
                        v = i * numColumns + pnRow[k];
                        p = elements[u];
                        elements[u] = elements[v];
                        elements[v] = p;
                    }
                }
            }

            //成功返回
            return true;

        }

        /*复矩阵求逆的全选主元高斯-约当法
         * mtxImag复矩阵的虚步矩阵，当前矩阵为复矩阵的实部
         * return bool型，求逆是否成功
         */
        public bool InvertGaussJordan(Matrix mtxImag)
        {
            int i, j, k, l, u, v, w;
            double p, q, s, t, d, b;

            //分配内存
            int[] pnRow = new int[numColumns];
            int[] pnCol = new int[numColumns];

            //消元
            for (k = 0; k <= numColumns - 1; k++)
            {
                d = 0.0;
                for (i = k; i <= numColumns - 1; i++)
                {
                    for (j = k; j <= numColumns - 1; j++)
                    {
                        u = i * numColumns + j;
                        p = elements[u] * elements[u] +
                            mtxImag.elements[u] * mtxImag.elements[u];
                        if (p > d)
                        {
                            d = p;
                            pnRow[k] = i;
                            pnCol[k] = j;
                        }
                    }
                }

                //失败
                if (d == 0.0)
                {
                    return false;
                }

                if (pnRow[k] != k)
                {
                    for (j = 0; j <= numColumns - 1; j++)
                    {
                        u = k * numColumns + j;
                        v = pnRow[k] * numColumns + j;
                        t = elements[u];
                        elements[u] = elements[v];
                        elements[v] = t;
                        t = mtxImag.elements[u];
                        mtxImag.elements[u] = mtxImag.elements[v];
                        mtxImag.elements[v] = t;
                    }
                }
                if (pnCol[k] != k)
                {
                    for (i = 0; i <= numColumns - 1; i++)
                    {
                        u = i * numColumns + k;
                        v = i * numColumns + pnCol[k];
                        t = elements[u];
                        elements[u] = elements[v];
                        elements[v] = t;
                        t = mtxImag.elements[u];
                        mtxImag.elements[u] = mtxImag.elements[v];
                        mtxImag.elements[v] = t;
                    }
                }

                l = k * numColumns + k;
                elements[l] = elements[l] / d;
                mtxImag.elements[l] = -mtxImag.elements[l] / d;
                for (j = 0; j <= numColumns - 1; j++)
                {
                    if (j != k)
                    {
                        u = k * numColumns + j;
                        p = elements[u] * elements[l];
                        q = mtxImag.elements[u] * mtxImag.elements[l];
                        s = (elements[u] + mtxImag.elements[u]) *
                            (elements[l] + mtxImag.elements[l]);
                        elements[u] = p - q;
                        mtxImag.elements[u] = s - p - q;
                    }
                }

                for (i = 0; i <= numColumns - 1; i++)
                {
                    if (i != k)
                    {
                        v = i * numColumns + k;
                        for (j = 0; j <= numColumns - 1; j++)
                        {
                            if (j != k)
                            {
                                u = k * numColumns + j;
                                w = i * numColumns + j;
                                p = elements[u] * elements[v];
                                q = mtxImag.elements[u] * mtxImag.elements[v];
                                s = (elements[u] + mtxImag.elements[u]) *
                                    (elements[v] + mtxImag.elements[v]);
                                t = p - q;
                                b = s - p - q;
                                elements[w] = elements[w] - t;
                                mtxImag.elements[w] = mtxImag.elements[w] - b;
                            }
                        }
                    }
                }

                for (i = 0; i <= numColumns - 1; i++)
                {
                    if (i != k)
                    {
                        u = i * numColumns + k;
                        p = elements[u] * elements[l];
                        q = mtxImag.elements[u] * mtxImag.elements[l];
                        s = (elements[u] + mtxImag.elements[u]) *
                            (elements[l] + mtxImag.elements[l]);
                        elements[u] = q - p;
                        mtxImag.elements[u] = p + q - s;
                    }
                }
            }

            //调整恢复行列次序
            for (k = numColumns - 1; k >= 0; k--)
            {
                if (pnCol[k] != k)
                {
                    for (j = 0; j <= numColumns - 1; j++)
                    {
                        u = k * numColumns + j;
                        v = pnCol[k] * numColumns + j;
                        t = elements[u];
                        elements[u] = elements[v];
                        elements[v] = t;
                        t = mtxImag.elements[u];
                        mtxImag.elements[u] = mtxImag.elements[v];
                        mtxImag.elements[v] = t;
                    }
                }

                if (pnRow[k] != k)
                {
                    for (i = 0; i <= numColumns - 1; i++)
                    {
                        u = i * numColumns + k;
                        v = i * numColumns + pnRow[k];
                        t = elements[u];
                        elements[u] = elements[v];
                        elements[v] = t;
                        t = mtxImag.elements[u];
                        mtxImag.elements[u] = mtxImag.elements[v];
                        mtxImag.elements[v] = t;
                    }
                }
            }

            //成功返回
            return true;
        }

        //对称正定矩阵的求逆
        //return bool型，求逆是否成功
        public bool InvertSsgj()
        {
            int i, j, k, m;
            double w, g;

            //临时内存
            double[] pTmp = new double[numColumns];

            //逐列处理
            for (k = 0; k <= numColumns - 1; k++)
            {
                w = elements[0];
                if (w == 0.0)
                {
                    return false;
                }

                m = numColumns - k - 1;
                for (i = 1; i <= numColumns - 1; i++)
                {
                    g = elements[i * numColumns];
                    pTmp[i] = g / w;
                    if (i <= m)
                        pTmp[i] = -pTmp[i];
                    for (j = 1; j <= i; j++)
                        elements[(i - 1) * numColumns + j - 1] = elements[i * numColumns + j] + g * pTmp[j];
                }

                elements[numColumns * numColumns - 1] = 1.0 / w;
                for (i = 1; i <= numColumns - 1; i++)
                    elements[(numColumns - 1) * numColumns + i - 1] = pTmp[i];
            }

            //行列调整
            for (i = 0; i <= numColumns - 2; i++)
                for (j = i + 1; j <= numColumns - 1; j++)
                    elements[i * numColumns + j] = elements[j * numColumns + i];

            return true;

        }

        /*托伯利兹矩阵求逆的爱兰特方法
         * return bool型，求逆是否成功
         */
        public bool InvertTrench()
        {
            int i, j, k;
            double a, s;

            //上三角元素
            double[] t = new double[numColumns];
            //下三角元素
            double[] tt = new double[numColumns];

            //上、下三角元素赋值
            for (i = 0; i < numColumns; ++i)
            {
                t[i] = GetElement(0, i);
                tt[i] = GetElement(i, 0);
            }

            //临时缓冲区
            double[] c = new double[numColumns];
            double[] r = new double[numColumns];
            double[] p = new double[numColumns];

            //非Toeplits矩阵，返回
            if (t[0] == 0.0)
            {
                return false;
            }

            a = t[0];
            c[0] = tt[1] / t[0];
            r[0] = t[1] / t[0];

            for (k = 0; k <= numColumns - 3; k++)
            {
                s = 0.0;
                for (j = 1; j <= k + 1; j++)
                    s = s + c[k + 1 - j] * tt[j];

                s = (s - tt[k + 2]) / a;
                for (i = 0; i <= k; i++)
                    p[i] = c[i] + s * r[k - i];

                c[k + 1] = -s;
                s = 0.0;
                for (j = 1; j <= k + 1; j++)
                    s = s + r[k + 1 - j] * c[j];

                s = (s - t[k + 2]) / a;
                for (i = 0; i <= k; i++)
                {
                    r[i] = r[i] + s * c[k - i];
                    c[k - i] = p[k - i];
                }

                r[k + 1] = -s;
                a = 0.0;
                for (j = 1; j <= k + 2; j++)
                    a = a + c[j] * c[j - 1];

                a = t[0] - a;

                //求解失败
                if (a == 0.0)
                {
                    return false;
                }
            }

            elements[0] = 1.0 / a;
            for (i = 0; i <= numColumns - 2; i++)
            {
                k = i + 1;
                j = (i + 1) * numColumns;
                elements[k] = -r[i] / a;
                elements[j] = -c[i] / a;
            }

            for (i = 0; i <= numColumns - 2; i++)
            {
                for (j = 0; j <= numColumns - 2; j++)
                {
                    k = (i + 1) * numColumns + j + 1;
                    elements[k] = elements[i * numColumns + j] - c[i] * elements[j + 1];
                    elements[k] = elements[k] +
                        c[numColumns - j - 2] * elements[numColumns - i - 1];
                }
            }
            return true;
        }

        //取矩阵的精度值  return double型，矩阵的精度值
        public double GetEps()
        {
            return eps;
        }

    }
}
