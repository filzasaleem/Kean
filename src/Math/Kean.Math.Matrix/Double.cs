﻿// 
//  Double.cs
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2011 Anders Frisk
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Geometry2D = Kean.Math.Geometry2D;
using Kean.Core.Extension;
namespace Kean.Math.Matrix
{
    public partial class Double :
        System.IEquatable<Double>
    {
        #region Properties
        Geometry2D.Integer.SizeValue dimensions;
        public Geometry2D.Integer.SizeValue Dimensions { get { return this.dimensions; } private set { this.dimensions = value; } }
        // Matrix elements are supposed to be in column major order 
        /// <summary>
        /// True if the matrix is a square matrix.
        /// </summary>
        public bool IsSquare { get { return this.Dimensions.Width == this.Dimensions.Height; } }
        /// <summary>
        /// Minimum of maxtrix dimensions.
        /// </summary>
        public int Order { get { return Kean.Math.Integer.Minimum(this.Dimensions.Width, this.Dimensions.Height); } }
        /// <summary>
        /// Frobenius norm of matrix
        /// </summary>
        public double Norm { get { return Kean.Math.Double.SquareRoot(this.ScalarProduct(this)); } }
        public double NormInfinity
        {
            get
            {
                double result = 0;
                for (int x = 0; x < this.Dimensions.Width; x++)
                    for (int y = 0; y < this.Dimensions.Height; y++)
                        result = Kean.Math.Double.Maximum(result, Kean.Math.Double.Absolute(this[x, y]));
                return result;
            }
        }
        /// <summary>
        /// Get or set an element in a matrix at position(x,y).
        /// </summary>
        /// <param name="x">Column number of a matrix.</param>
        /// <param name="y">Row number of a matrix.</param>
        /// <returns></returns>
        public double this[int x, int y]
        {
            get { return this.elements[this.Index(x, y)]; }
            set { this.elements[this.Index(x, y)] = value; }
        }
        int Index(int x, int y)
        {
            return x * this.Dimensions.Height + y; // Column major order 
            // Use Y * this.Dimensions.Width + X for row major order
        }
        #endregion
        #region Private Fields
        double[] elements;
        #endregion
        #region Constructors
        public Double() : this(0) { }
        public Double(int order) : this(order, order) { }
        public Double(int width, int height) : this(new Geometry2D.Integer.SizeValue(width, height)) { }
        public Double(Geometry2D.Integer.SizeValue dimensions) : this(dimensions, new double[dimensions.Area]) { }
        public Double(int width, int height, double[] elements) : this(new Geometry2D.Integer.SizeValue(width, height), elements) { }
        public Double(Geometry2D.Integer.SizeValue dimensions, double[] elements)
        {
            this.Dimensions = dimensions;
            int minimum = Kean.Math.Integer.Minimum(elements.Length, this.Dimensions.Area);
            this.elements = new double[this.Dimensions.Area];
            Array.Copy(elements, 0, this.elements, 0, minimum);
        }
        #endregion
        #region Matrix Geometry
        /// <summary>
        /// Distance induced by the Frobenius norm.
        /// </summary>
        /// <param name="other">Other matrix.</param>
        /// <returns>Distance between current and other matrix.</returns>
        public double Distance(Double other)
        {
            return (this - other).Norm;
        }
        public double ScalarProduct(Double other)
        {
            if (this.Dimensions != other.Dimensions)
                new Exception.InvalidDimensions();
            double result = 0;
            for (int x = 0; x < this.Dimensions.Width; x++)
                for (int y = 0; y < this.Dimensions.Height; y++)
                    result += this[x, y] * other[x, y];
            return result;
        }
        #endregion
        #region Static Arithmetic: Matrix and Scalar Operators
        /// <summary>
        /// Addition of matrices.
        /// </summary>
        /// <param name="left">Left matrix in the addition.</param>
        /// <param name="right">Right matrix in the addition.</param>
        /// <returns>Sum of left and right matrices.</returns>
        public static Double operator +(Double left, Double right)
        {
            if (left.Dimensions != right.Dimensions)
                new Exception.InvalidDimensions();
            Double result = new Double(left.Dimensions);
            for (int i = 0; i < result.elements.Length; i++)
                result.elements[i] = left.elements[i] + right.elements[i];
            return result;
        }
        /// <summary>
        /// Multiplication of matrices.
        /// </summary>
        /// <param name="left">Left matrix in the multiplication.</param>
        /// <param name="right">Right matrix in the multiplication.</param>
        /// <returns>Product of left and right matrices.</returns>
        public static Double operator *(Double left, Double right)
        {
            if (left.Dimensions.Width != right.Dimensions.Height)
                new Exception.InvalidDimensions();
            Double result = new Double(right.Dimensions.Width, left.Dimensions.Height);
            for (int x = 0; x < right.Dimensions.Width; x++)
                for (int y = 0; y < left.Dimensions.Height; y++)
                    for (int z = 0; z < left.Dimensions.Width; z++)
                        result[x, y] = result[x, y] + left[z, y] * right[x, z];
            return result;
        }
        /// Difference between matrices.
        /// </summary>
        /// <param name="left">Left matrix in the differerence.</param>
        /// <param name="right">Right matrix in the differerence.</param>
        /// <returns>Difference of left and right matrices.</returns>
        public static Double operator -(Double left, Double right)
        {
            if (left.Dimensions != right.Dimensions)
                new Exception.InvalidDimensions();
            Double result = new Double(left.Dimensions);
            for (int i = 0; i < result.elements.Length; i++)
                result.elements[i] = left.elements[i] - right.elements[i];
            return result;

        }
        /// <summary>
        /// Multiplication between scalar and matrix.
        /// </summary>
        /// <param name="left">Left scalar in the multiplication.</param>
        /// <param name="right">Right matrix in the multiplication.</param>
        /// <returns>Product of scalar and matrix.</returns>
        public static Double operator *(double left, Double right)
        {
            Double result = new Double(right.Dimensions);
            for (int i = 0; i < result.elements.Length; i++)
                result.elements[i] = left * right.elements[i];
            return result;
        }
        /// <summary>
        /// Multiplication between scalar and matrix.
        /// </summary>
        /// <param name="left">Left matrix in the multiplication.</param>
        /// <param name="right">Right scalar in the multiplication.</param>
        /// <returns>Product of matrix and  scalar.</returns>
        public static Double operator *(Double left, double right)
        {
            return right * left;
        }
        /// <summary>
        /// Division between scalar and matrix.
        /// </summary>
        /// <param name="left">Left matrix in the multiplication.</param>
        /// <param name="right">Right scalar in the multiplication.</param>
        /// <returns>Quotient between matrix and scalar.</returns>
        public static Double operator /(Double left, double right)
        {
            return left * (1 / right);
        }
        /// <summary>
        /// Negation of a matrix.
        /// </summary>
        /// <param name="value">Matrix to be negated.</param>
        /// <returns>Negated matrix.</returns>
        public static Double operator -(Double value)
        {
            return (-1) * value;
        }
        #endregion
        #region Matrix Methods
        /// <summary>
        /// Tranpose matrix. Creates a new matrix being the transpose of the current matrix.
        /// </summary>
        /// <returns>Return current matrix tranposed.</returns>
        public Double Transpose()
        {
            Double result = new Double(this.Dimensions.Height, this.Dimensions.Width);
            for (int x = 0; x < result.Dimensions.Width; x++)
                for (int y = 0; y < result.Dimensions.Height; y++)
                    result[x, y] = this[y, x];
            return result;
        }
        /// <summary>
        /// Adjoint matrix. Creates a new matrix which is the adjoint of the current matrix.
        /// </summary>
        /// <returns>Adjoint of current matrix.</returns>
        public Double Adjoint()
        {
            Double result = new Double(this.Dimensions.Width, this.Dimensions.Height);
            for (int x = 0; x < result.Dimensions.Width; x++)
                for (int y = 0; y < result.Dimensions.Height; y++)
                    result[x, y] = Kean.Math.Double.Power(-1, x + 1 + y + 1) * this.Minor(y, x).Determinant();
            return result;

        }
        /// <summary>
        /// Minor of the current matrix.
        /// </summary>
        /// <param name="x">Column position in the matrix.</param>
        /// <param name="y">Row position in the matrix.</param>
        /// <returns>Return the minor of a matrix at position (x,y).</returns>
        public Double Minor(int x, int y)
        {
            if (this.Dimensions.Width < 1 || this.Dimensions.Height < 1)
                new Exception.InvalidDimensions();
            Double result = new Double(this.Dimensions.Width - 1, this.Dimensions.Height - 1);
            for (int xx = 0; xx < x; xx++)
            {
                for (int yy = 0; yy < y; yy++)
                    result[xx, yy] = this[xx, yy];
                for (int yy = y + 1; yy < this.Dimensions.Height; yy++)
                    result[xx, yy - 1] = this[xx, yy];
            }
            for (int xx = x + 1; xx < this.Dimensions.Width; xx++)
            {
                for (int yy = 0; yy < y; yy++)
                    result[xx - 1, yy] = this[xx, yy];
                for (int yy = y + 1; yy < this.Dimensions.Height; yy++)
                    result[xx - 1, yy - 1] = this[xx, yy];
            }
            return result;
        }
        /// <summary>
        /// Platformct the submatrix being the rectangular part of current matrix with top left corner (x,y).
        /// </summary>
        /// <param name="left">Column position.</param>
        /// <param name="top">Row position.</param>
        /// <returns>Return the extract submatrix with top left corner (x,y).</returns>
        public Double Platformct(int left, int top)
        {
            return this.Platformct(left, this.Dimensions.Width, top, this.Dimensions.Height);
        }
        public Double Platformct(int left, int right, int top, int bottom)
        {
            if (
                left < 0 || left > this.Dimensions.Width ||
                right < 0 || right > this.Dimensions.Width ||
                top < 0 || top > this.Dimensions.Height ||
                bottom < 0 || bottom > this.Dimensions.Height)
                throw new Exception.InvalidDimensions();
            Double result = new Double(right - left, bottom - top);
            for (int x = left; x < right; x++)
                for (int y = top; y < bottom; y++)
                    result[x - left, y - top] = this[x, y];
            return result;
        }
        /// <summary>
        /// Paste a submatrix into a copy of the current matrix. The submatrix is pasted a position top left corner
        /// (x,y).
        /// </summary>
        /// <param name="x">Column position.</param>
        /// <param name="y">Row position.</param>
        /// <param name="submatrix">Matrix to be pasted into current matrix.</param>
        /// <returns>Return new matrix with submatrix pasted.</returns>
        public Double Paste(int left, int top, Double submatrix)
        {
            if (
                left < 0 || left > this.Dimensions.Width ||
                top < 0 || top > this.Dimensions.Height)
                throw new Exception.InvalidDimensions();
            Double result = this.Copy();
            for (int x = 0; x < submatrix.Dimensions.Width; x++)
                for (int y = 0; y < submatrix.Dimensions.Height; y++)
                    result[x + left, y + top] = submatrix[x, y];
            return result;
        }
        /// <summary>
        /// Paste a submatrix into a copy of the current matrix. The submatrix is pasted a position top left corner (x,y).
        /// This method does not create a new matrix but instead keeps the current matrix.
        /// </summary>
        /// <param name="left">Column position.</param>
        /// <param name="top">Row position.</param>
        /// <param name="submatrix">Matrix to paste into the current matrix.</param>
        public void Set(int left, int top, Double submatrix)
        {
            if (
                left < 0 || left > this.Dimensions.Width ||
                top < 0 || top > this.Dimensions.Height)
                throw new Exception.InvalidDimensions();
            for (int x = 0; x < submatrix.Dimensions.Width; x++)
                for (int y = 0; y < submatrix.Dimensions.Height; y++)
                    this[x + left, y + top] = submatrix[x, y];
        }
        /// <summary>
        /// Sets a region in a matrix to zero.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        public void Clear(int left, int right, int top, int bottom)
        {
            if (
                left < 0 || left > this.Dimensions.Width ||
                right < 0 || right > this.Dimensions.Width ||
                top < 0 || top > this.Dimensions.Height ||
                bottom < 0 || bottom > this.Dimensions.Height)
                throw new Exception.InvalidDimensions();
            for (int x = left; x < right; x++)
                for (int y = top; y < bottom; y++)
                    this[x, y] = 0;
        }
        public void Clear()
        {
            for (int x = 0; x < this.Dimensions.Width; x++)
                for (int y = 0; y < this.Dimensions.Height; y++)
                    this[x, y] = 0;
        }
        /// <summary>
        /// Creates a copy of the current matrix.
        /// </summary>
        /// <returns>Return a copy of the current matrix.</returns>
        public Double Copy()
        {
            Double result = new Double(this.Dimensions);
            Array.Copy(this.elements, result.elements, this.elements.Length);
            return result;
        }
        public Double Kronecker(Double other)
        {
            Double[,] blocks = new Double[this.Dimensions.Width, this.Dimensions.Height];
            for (int x = 0; x < this.Dimensions.Width; x++)
                for (int y = 0; y < this.Dimensions.Height; y++)
                    blocks[x, y] = this[x, y] * other;
            return Double.Block(blocks);
        }
        #endregion
        #region Static Constructors
        /// <summary>
        /// Creates an identity matrix of given order.
        /// </summary>
        /// <param name="order">Order of matrix to be created.</param>
        /// <returns>Identity matrix of given order.</returns>
        public static Double Identity(int order)
        {
            Double result = new Double(order, order);
            for (int i = 0; i < order; i++)
                result[i, i] = 1;
            return result;
        }
        /// <summary>
        /// Creates a diagonal block matrix with block given. Outside blocks the matrix has zero elements.
        /// </summary>
        /// <param name="matrices">Matrices to be on the diagonal of the created matrix.</param>
        /// <returns>Block diagonal matrix.</returns>
        public static Double Diagonal(params Double[] matrices)
        {
            int width = 0;
            int height = 0;
            for (int i = 0; i < matrices.Length; i++)
            {
                width += matrices[i].Dimensions.Width;
                height += matrices[i].Dimensions.Height;
            }
            Double result = new Double(width, height);
            int k = 0;
            int l = 0;
            for (int i = 0; i < matrices.Length; i++)
            {
                result.Set(k, l, matrices[i]);
                k += matrices[i].Dimensions.Width;
                l += matrices[i].Dimensions.Height;
            }
            return result;
        }
        /// <summary>
        /// Create a matrix from a two-dimensional array of matrices 
        /// such that the given matrices become sub-block-matrices in the 
        /// construted matrix.
        /// </summary>
        /// <param name="matrices"></param>
        /// <returns></returns>
        public static Double Block(Double[,] matrices)
        {
            Double result;
            int width = 0;
            int height = 0;
            int blockWidth = matrices.GetLength(0);
            int blockHeight = matrices.GetLength(1);
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    if (matrices[x, 0].Dimensions.Width != matrices[x, y].Dimensions.Width ||
                    matrices[0, y].Dimensions.Height != matrices[x, y].Dimensions.Height)
                        throw new Exception.InvalidDimensions();
                }
            }
            for (int x = 0; x < blockWidth; x++)
                width += matrices[x, 0].Dimensions.Width;
            for (int y = 0; y < blockHeight; y++)
                height += matrices[0, y].Dimensions.Height;
            result = new Double(width, height);
            width = 0;
            height = 0;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    Double block = matrices[x, y];
                    for (int xx = 0; xx < block.Dimensions.Width; xx++)
                        for (int yy = 0; yy < block.Dimensions.Height; yy++)
                            result[width + xx, height + yy] = block[xx, yy];
                    height += matrices[0, y].Dimensions.Height;
                }
                width += matrices[x, 0].Dimensions.Width;
                height = 0;
            }
            return result;
        }
        /// <summary>
        /// Column basis vector of given length and with a one a given index.
        /// </summary>
        /// <param name="length">Length of column to be created.</param>
        /// <param name="index">Index to set the one.</param>
        /// <returns>Column vector matrix of given length and a one at given index.</returns>
        public static Double Basis(int length, int index)
        {
            return Double.Basis(length, index, true);
        }
        /// <summary>
        /// Column / Row basis vector of given length and with a one a given index.
        /// </summary>
        /// <param name="length">Length of column to be created.</param>
        /// <param name="index">Index to set the one.</param>
        /// <param name="column">Column vector if set to true, row vector if set to false.</param>
        /// <returns>Column / Row vector matrix of given length and a one at given index.</returns>
        public static Double Basis(int length, int index, bool column)
        {
            Double result = new Double(1, length);
            result[0, index] = 1;
            if (!column)
                result = result.Transpose();
            return result;
        }
        #endregion
        #region Object overides and IEquatable<Double>
        public override bool Equals(object other)
        {
            return (other is Double) && this.Equals(other as Double);
        }
        // other is not null here.
        public bool Equals(Double other)
        {
            bool result = this.Dimensions == other.Dimensions;
            if (result)
            {
                for (int x = 0; x < this.Dimensions.Width; x++)
                    for (int y = 0; y < this.Dimensions.Height; y++)
                        result &= this[x, y] == other[x, y];
            }
            return result;
        }
        public override int GetHashCode()
        {
            int result = this.Dimensions.GetHashCode();
            for (int i = 0; i < this.Dimensions.Area; i++)
                result ^= this.elements[i].GetHashCode();
            return result;
        }
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int y = 0; y < this.Dimensions.Height; y++)
                for (int x = 0; x < this.Dimensions.Width; x++)
                {
                    builder.Append((Kean.Math.Double.ToString(this[x, y])));
                    builder.Append((x == this.Dimensions.Width - 1) ? ((y == this.Dimensions.Height - 1) ? "" : "; ") : ", ");
                }
            return builder.ToString();
        }
        #endregion
        #region Comparison Functions and IComparable<Double>
        public static bool operator ==(Double left, Double right)
        {
            return
                left.Same(right) || (left.NotNull() && right.NotNull()) &&
                left.Equals(right);
        }
        public static bool operator !=(Double left, Double right)
        {
            return !(left == right);
        }
        #endregion
        #region Casts
        public static explicit operator Double(double[] value)
        {
            Double result = new Double(1, value.Length);
            Array.Copy(value, result.elements, value.Length);
            return result;
        }
        public static explicit operator double[](Double value)
        {
            double[] result = new double[value.elements.Length];
            Array.Copy(value.elements, result, result.Length);
            return result;
        }
        public static explicit operator double[,](Double value)
        {
            double[,] result = new double[value.Dimensions.Width, value.Dimensions.Height];
            for (int x = 0; x < value.Dimensions.Width; x++)
                for (int y = 0; y < value.Dimensions.Height; y++)
                    result[x, y] = value[x, y];
            return result;
        }
        public static explicit operator Double(double[,] value)
        {
            Double result = new Double(value.GetLength(0), value.GetLength(1));
            for (int x = 0; x < result.Dimensions.Width; x++)
                for (int y = 0; y < result.Dimensions.Height; y++)
                    result[x, y] = value[x, y];
            return result;
        }
        #endregion
    }
}