using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomControl
{
   public  class MySpline
    {
        static private int _MaxVoltageAxis = 50;
        static private int _CurvesPaneWidth = 180;
        public static I_V_Point[] Spline(I_V_Point[] oriPoints)
        {
            try
            {
                if (oriPoints.Length < 3) throw new Exception("参考点不能少于3个。");
                double maxVoltage = oriPoints[oriPoints.Length - 1].Voltage;
                int pointCount = (int)(maxVoltage * _CurvesPaneWidth / _MaxVoltageAxis - 0.5);

                int cNum = oriPoints.Length - 1; //N
                double[] H = new double[cNum + 1]; //dX1[N + 1]
                double[] A = new double[cNum + 1]; //dx2[N + 1]
                double[] B = new double[cNum + 1]; //B[N +1]
                double[] C = new double[cNum + 1]; //C[N +1]
                double[] D = new double[cNum + 1]; //D[N +1]

                #region 计算中间参数
                for (int i = 1; i <= cNum; i++)
                {
                    H[i - 1] = oriPoints[i].Voltage - oriPoints[i - 1].Voltage;// 横坐标之差 
                }

                for (int i = 1; i <= cNum - 1; i++)
                {
                    A[i] = H[i - 1] / (H[i - 1] + H[i]); //  中线/底线
                    B[i] = 3 * ((1 - A[i]) * (oriPoints[i].Current - oriPoints[i - 1].Current) / H[i - 1]
                        + A[i] * (oriPoints[i + 1].Current - oriPoints[i].Current) / H[i]);
                }
                A[0] = 1;
                A[cNum] = 0;
                B[0] = 3 * (oriPoints[1].Current - oriPoints[0].Current) / H[0];//  纵坐标之差/横坐标之差 = 斜率
                B[cNum] = 3 * (oriPoints[cNum].Current - oriPoints[cNum - 1].Current) / H[cNum - 1];

                for (int i = 0; i <= cNum; i++) D[i] = 2;

                for (int i = 0; i <= cNum; i++) C[i] = 1 - A[i];

                for (int i = 1; i <= cNum; i++)
                {

                    if (Math.Abs(D[i]) <= 0.000001)
                    {
                        throw new Exception("数据错误，无解");
                    }
                    A[i - 1] = A[i - 1] / D[i - 1];
                    B[i - 1] = B[i - 1] / D[i - 1];
                    D[i] = A[i - 1] * (-C[i]) + D[i];
                    B[i] = -C[i] * B[i - 1] + B[i];
                }
                B[cNum] = B[cNum] / D[cNum];
                for (int i = 1; i <= cNum; i++)
                {
                    B[cNum - i] = B[cNum - i] - A[cNum - i] * B[cNum - i + 1];
                }
                #endregion

                #region 计算返回值
                I_V_Point[] rsltPoints = new I_V_Point[pointCount];
                double dbStep = (oriPoints[cNum].Voltage - oriPoints[0].Voltage) / (pointCount - 1);
                rsltPoints[0] = oriPoints[0];
                for (int i = 1; i < pointCount; ++i)
                {
                    rsltPoints[i] = new I_V_Point();
                    double xValue = oriPoints[0].Voltage + dbStep * i;
                    rsltPoints[i].Voltage = xValue;

                    int sPos = 0;
                    if (xValue < oriPoints[0].Voltage) sPos = 0;
                    else if (xValue > oriPoints[cNum].Voltage) sPos = cNum - 1;
                    else
                    {
                        for (int j = 1; j <= cNum; j++)
                        {
                            if (xValue <= oriPoints[j].Voltage)
                            {
                                sPos = j - 1;
                                break;
                            }
                        }
                    }

                    double diffPrev = xValue - oriPoints[sPos].Voltage;
                    double diffNext = oriPoints[sPos + 1].Voltage - xValue;
                    double yVlaue = 0;
                    yVlaue = (3 * Math.Pow(diffNext, 2) - 2 * Math.Pow(diffNext, 3) / H[sPos]) * oriPoints[sPos].Current
                        + (3 * Math.Pow(diffPrev, 2) - 2 * Math.Pow(diffPrev, 3) / H[sPos]) * oriPoints[sPos + 1].Current
                        + (H[sPos] * Math.Pow(diffNext, 2) - Math.Pow(diffNext, 3)) * B[sPos]
                        - (H[sPos] * Math.Pow(diffPrev, 2) - Math.Pow(diffPrev, 3)) * B[sPos + 1];
                    yVlaue = yVlaue / (Math.Abs((H[sPos] * H[sPos])));//原来是   yVlaue = yVlaue /  (H[sPos] * H[sPos]);
                    rsltPoints[i].Current = yVlaue;
                }
                #endregion

                return rsltPoints;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public static I_V_Point[] Spline1(I_V_Point[] oriPoints)
        {
            double maxVoltage = oriPoints[oriPoints.Length - 1].Voltage;
            int len = (int)(maxVoltage * _CurvesPaneWidth / _MaxVoltageAxis - 0.5);
            Coefs[] coefsArray = CalcCoefs(oriPoints);
            I_V_Point[] pointArray = new I_V_Point[len];
            for (int i = 0; i < len; i++)
            {
                double xVal = i * _MaxVoltageAxis / _CurvesPaneWidth;
                int sPos = 0;
                if (xVal < oriPoints[0].Voltage) sPos = 0;
                else if (xVal > oriPoints[len - 1].Voltage) sPos = len - 2;
                else
                {
                    for (int j = 1; j < len; j++)
                    {
                        if (xVal <= oriPoints[j].Voltage)
                        {
                            sPos = j - 1;
                            break;
                        }
                    }
                }
                pointArray[i] = new I_V_Point();
                pointArray[i].Voltage = xVal;
                pointArray[i].Current = EvaluateValue(coefsArray[sPos], xVal);
            }

            return pointArray;
        }

        private static Coefs[] CalcCoefs(I_V_Point[] oriPoints)
        {
            int len = oriPoints.Length;
            Coefs[] CoefsArray = new Coefs[len - 1];
            for (int i = 0; i < len - 1; i++)
            {
                Coefs coef = new Coefs();
                double dy = oriPoints[i + 1].Current - oriPoints[i].Current;
                double dx = oriPoints[i + 1].Voltage - oriPoints[i].Voltage;
                double divdif = dy / dx;

                coef.Coef0 = oriPoints[i].Current;
                coef.Coef1 = 00;
                coef.Coef2 = 00;
                coef.Coef3 = 00;

                //yi=y(:,ind).'; 
                //dx = diff(x); 
                //divdif = diff(yi)./dx;
                //else % set up the sparse, tridiagonal, linear system for the slopes at  X .
                //   b=zeros(n,yd);
                //   b(2:n-1)=3*(dx(2:n-1).*divdif(1:n-2)+dx(1:n-2).*divdif(2:n-1)); 
                //   x31=x(3)-x(1);xn=x(n)-x(n-2);
                //   b(1)=((dx(1)+2*x31)*dx(2)*divdif(1)+dx(1)^2*divdif(2))/x31;
                //   b(n)=(dx(n-1)^2*divdif(n-2)+(2*xn+dx(n-1))*dx(n-2)*divdif(n-1))/xn;

                //   c = spdiags([ [dx(2:n-1);xn;0] ...
                //        [dx(2);2*[dx(2:n-1)+dx(1:n-2)];dx(n-2)] ...
                //        [0;x31;dx(1:n-2)] ],[-1 0 1],n,n);

                //   % sparse linear equation solution for the slopes
                //   mmdflag = spparms('autommd');
                //   spparms('autommd',0);
                //   s=c\b;
                //   spparms('autommd',mmdflag);
                //   % convert to pp form
                //   c4=(s(1:n-1)+s(2:n)-2*divdif(1:n-1))./dx;
                //   c3=(divdif(1:n-1)-s(1:n-1))./dx - c4;
                //   pp=mkpp(x.', ...
                //      reshape([(c4./dx).' c3.' s(1:n-1).' yi(1:n-1).'], ...
                //               (n-1)*yd,4),yd);
            }

            return CoefsArray;
        }


        private static double EvaluateValue(Coefs coefs, double xVal)
        {
            double ret = coefs.Coef3 * xVal * xVal * xVal
                + coefs.Coef2 * xVal * xVal + coefs.Coef1 * xVal + coefs.Coef0;
            return ret;
        }

        class Coefs
        {
            public double Coef0 { get; set; }
            public double Coef1 { get; set; }
            public double Coef2 { get; set; }
            public double Coef3 { get; set; }
        }
    }
}
