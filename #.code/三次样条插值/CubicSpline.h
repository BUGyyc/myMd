#include "TriDiagonalMatrixF.h"

const double esp3 = 1e-6;
class CubicSpline
{
public:
#pragma region Fields

	// N-1 spline coefficients for N points
	std::vector<double>  a;
	std::vector<double>  b;

	// Save the original x and y for Eval
	
	std::vector<double> xOrig;
	std::vector<double> yOrig;

#pragma endregion

#pragma region Ctor

	/// <summary>
	/// Default ctor.
	/// </summary>
	CubicSpline()
	{
		_lastIndex = 0;
	}

	/// <summary>
	/// Construct and call Fit.
	/// </summary>
	/// <param name="x">Input. X coordinates to fit.</param>
	/// <param name="y">Input. Y coordinates to fit.</param>
	/// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
	/// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/*public CubicSpline(double[] x, double[] y, double startSlope = double.NaN, double endSlope = double.NaN, bool debug = false)
	{
		Fit(x, y, startSlope, endSlope, debug);
	}*/

#pragma endregion

#pragma region Private Methods

	/// <summary>
	/// Throws if Fit has not been called.
	/// </summary>
	void CheckAlreadyFitted()
	{
		assert(a.size() > 0);
	}

	int _lastIndex;

	/// <summary>
	/// Find where in xOrig the specified x falls, by simultaneous traverse.
	/// This allows xs to be less than x[0] and/or greater than x[n-1]. So allows extrapolation.
	/// This keeps state, so requires that x be sorted and xs called in ascending order, and is not multi-thread safe.
	/// </summary>
	int GetNextXIndex(double x)
	{
		if (x < xOrig[_lastIndex])
		{
			assert(0 && "The X values to evaluate must be sorted.");
		}

		while ((_lastIndex < xOrig.size() - 2) && (x > xOrig[_lastIndex + 1]))
		{
			_lastIndex++;
		}

		return _lastIndex;
	}

	/// <summary>
	/// Evaluate the specified x value using the specified spline.
	/// </summary>
	/// <param name="x">The x value.</param>
	/// <param name="j">Which spline to use.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/// <returns>The y value.</returns>
	double EvalSpline(double x, int j, bool debug = false)
	{
		double dx = xOrig[j + 1] - xOrig[j];
		double t = (x - xOrig[j]) / dx;
		double y = (1 - t) * yOrig[j] + t * yOrig[j + 1] + t * (1 - t) * (a[j] * (1 - t) + b[j] * t); // equation 9
		//if (debug) Console.WriteLine("xs = {0}, j = {1}, t = {2}", x, j, t);
		return y;
	}

#pragma endregion

#pragma region Fit*

	/// <summary>
	/// Fit x,y and then eval at points xs and return the corresponding y's.
	/// This does the "natural spline" style for ends.
	/// This can extrapolate off the ends of the splines.
	/// You must provide points in X sort order.
	/// </summary>
	/// <param name="x">Input. X coordinates to fit.</param>
	/// <param name="y">Input. Y coordinates to fit.</param>
	/// <param name="xs">Input. X coordinates to evaluate the fitted curve at.</param>
	/// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
	/// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/// <returns>The computed y values for each xs.</returns>
	void FitAndEval(std::vector<double> & x, std::vector<double> & y, std::vector<double> & xs, int outputCount, std::vector<double> & xxs, bool debug = false)
	{
		Fit(x, y, debug);
		Eval(xs, xxs, debug);
	}

	/// <summary>
	/// Compute spline coefficients for the specified x,y points.
	/// This does the "natural spline" style for ends.
	/// This can extrapolate off the ends of the splines.
	/// You must provide points in X sort order.
	/// </summary>
	/// <param name="x">Input. X coordinates to fit.</param>
	/// <param name="y">Input. Y coordinates to fit.</param>
	/// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
	/// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	void Fit(std::vector<double> &  x, std::vector<double> &  y, bool debug = false)
	{
		// Save x and y for eval
		xOrig.assign(x.begin(), x.end());
		yOrig.assign(y.begin(), y.end());

		int n = x.size();
		std::vector<double> r(n); // the right hand side numbers: wikipedia page overloads b

		TriDiagonalMatrixF m(n);
		double dx1, dx2, dy1, dy2;

		// First row is different (equation 16 from the article)
		dx1 = x[1] - x[0];
		m.C[0] = 1.0f / dx1;
		m.B[0] = 2.0f * m.C[0];
		r[0] = 3 * (y[1] - y[0]) / (dx1 * dx1);

		// Body rows (equation 15 from the article)
		for (int i = 1; i < n - 1; i++)
		{
			dx1 = x[i] - x[i - 1];
			dx2 = x[i + 1] - x[i];

			m.A[i] = 1.0f / dx1;
			m.C[i] = 1.0f / dx2;
			m.B[i] = 2.0f * (m.A[i] + m.C[i]);

			dy1 = y[i] - y[i - 1];
			dy2 = y[i + 1] - y[i];
			r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
		}

		// Last row also different (equation 17 from the article)
		dx1 = x[n - 1] - x[n - 2];
		dy1 = y[n - 1] - y[n - 2];
		m.A[n - 1] = 1.0f / dx1;
		m.B[n - 1] = 2.0f * m.A[n - 1];
		r[n - 1] = 3 * (dy1 / (dx1 * dx1));

		// k is the solution to the matrix
		std::vector<double> k;
		m.Solve(r, k);

		// a and b are each spline's coefficients
		a.resize(n - 1);
		b.resize(n - 1);

		for (int i = 1; i < n; i++)
		{
			dx1 = x[i] - x[i - 1];
			dy1 = y[i] - y[i - 1];
			a[i - 1] = k[i - 1] * dx1 - dy1; // equation 10 from the article
			b[i - 1] = -k[i] * dx1 + dy1; // equation 11 from the article
		}
	}

#pragma endregion

#pragma region Eval*

	/// <summary>
	/// Evaluate the spline at the specified x coordinates.
	/// This can extrapolate off the ends of the splines.
	/// You must provide X's in ascending order.
	/// The spline must already be computed before calling this, meaning you must have already called Fit() or FitAndEval().
	/// </summary>
	/// <param name="x">Input. X coordinates to evaluate the fitted curve at.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/// <returns>The computed y values for each x.</returns>
	void Eval(std::vector<double> &x, std::vector<double> &xxs, bool debug = false)
	{
		CheckAlreadyFitted();

		int n = x.size();
		xxs.resize(n);
		_lastIndex = 0; // Reset simultaneous traversal in case there are multiple calls

		for (int i = 0; i < n; i++)
		{
			// Find which spline can be used to compute this x (by simultaneous traverse)
			int j = GetNextXIndex(x[i]);

			// Evaluate using j'th spline
			xxs[i] = EvalSpline(x[i], j, debug);
		}
	}

	/// <summary>
	/// Evaluate (compute) the slope of the spline at the specified x coordinates.
	/// This can extrapolate off the ends of the splines.
	/// You must provide X's in ascending order.
	/// The spline must already be computed before calling this, meaning you must have already called Fit() or FitAndEval().
	/// </summary>
	/// <param name="x">Input. X coordinates to evaluate the fitted curve at.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/// <returns>The computed y values for each x.</returns>
	//public double[] EvalSlope(double[] x, bool debug = false)
	//{
	//	CheckAlreadyFitted();

	//	int n = x.Length;
	//	double[] qPrime = new double[n];
	//	_lastIndex = 0; // Reset simultaneous traversal in case there are multiple calls

	//	for (int i = 0; i < n; i++)
	//	{
	//		// Find which spline can be used to compute this x (by simultaneous traverse)
	//		int j = GetNextXIndex(x[i]);

	//		// Evaluate using j'th spline
	//		double dx = xOrig[j + 1] - xOrig[j];
	//		double dy = yOrig[j + 1] - yOrig[j];
	//		double t = (x[i] - xOrig[j]) / dx;

	//		// From equation 5 we could also compute q' (qp) which is the slope at this x
	//		qPrime[i] = dy / dx
	//			+ (1 - 2 * t) * (a[j] * (1 - t) + b[j] * t) / dx
	//			+ t * (1 - t) * (b[j] - a[j]) / dx;

	//		if (debug) Console.WriteLine("[{0}]: xs = {1}, j = {2}, t = {3}", i, x[i], j, t);
	//	}

	//	return qPrime;
	//}

#pragma endregion

#pragma region Static Methods

	/// <summary>
	/// Static all-in-one method to fit the splines and evaluate at X coordinates.
	/// </summary>
	/// <param name="x">Input. X coordinates to fit.</param>
	/// <param name="y">Input. Y coordinates to fit.</param>
	/// <param name="xs">Input. X coordinates to evaluate the fitted curve at.</param>
	/// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
	/// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
	/// <param name="debug">Turn on console output. Default is false.</param>
	/// <returns>The computed y values for each xs.</returns>
	/*public static double[] Compute(double[] x, double[] y, double[] xs, double startSlope = double.NaN, double endSlope = double.NaN, bool debug = false)
	{
		CubicSpline spline = new CubicSpline();
		return spline.FitAndEval(x, y, xs, startSlope, endSlope, debug);
	}*/

	/// <summary>
	/// Fit the input x,y points using the parametric approach, so that y does not have to be an explicit
	/// function of x, meaning there does not need to be a single value of y for each x.
	/// </summary>
	/// <param name="x">Input x coordinates.</param>
	/// <param name="y">Input y coordinates.</param>
	/// <param name="nOutputPoints">How many output points to create.</param>
	/// <param name="xs">Output (interpolated) x values.</param>
	/// <param name="ys">Output (interpolated) y values.</param>
	/// <param name="firstDx">Optionally specifies the first point's slope in combination with firstDy. Together they
	/// are a vector describing the direction of the parametric spline of the starting point. The vector does
	/// not need to be normalized. If either is NaN then neither is used.</param>
	/// <param name="firstDy">See description of dx0.</param>
	/// <param name="lastDx">Optionally specifies the last point's slope in combination with lastDy. Together they
	/// are a vector describing the direction of the parametric spline of the last point. The vector does
	/// not need to be normalized. If either is NaN then neither is used.</param>
	/// <param name="lastDy">See description of dxN.</param>
	static void FitParametric(std::vector<double> &x, std::vector<double> &y, int nOutputPoints, std::vector<double> &xs, std::vector<double> &ys)
	{
		// Compute distances
		//整个长度
		int n = x.size();
		//根据长度创建一个新的数组
		std::vector<double>dists(n);
		//初始化第一个
		dists[0] = 0;
		//累计间距
		double totalDist = 0;

		for (int i = 1; i < n; i++)
		{
			double dx = x[i] - x[i - 1];
			double dy = y[i] - y[i - 1];
			//得到两点间距长度
			double dist = sqrt(dx * dx + dy * dy);
			totalDist += dist;
			//记录间距
			dists[i] = totalDist;
		}

		// Create 'times' to interpolate to
		//得到nOutputPoints的间距时间
		double dt = totalDist / (nOutputPoints - 1);
		
		//按时间创建数组
		std::vector<double> times(nOutputPoints);
		times[0] = 0;

		for (int i = 1; i < nOutputPoints; i++)
		{
			times[i] = times[i - 1] + dt;
		}

		xs.resize(nOutputPoints);
		ys.resize(nOutputPoints);
		// Spline fit both x and y to times
		CubicSpline xSpline;
		xSpline.FitAndEval(dists, x, times, nOutputPoints, xs);

		CubicSpline ySpline;
		ySpline.FitAndEval(dists, y, times, nOutputPoints, ys);
	}
#define min(a, b) ((a < b) ? (a) : (b))
#define max(a, b) ((a > b) ? (a) : (b))
	static void FitParametricDt(std::vector<double> & inputX, std::vector<double> & inputY, double fGap, double snakelength, int &outputCount, std::vector<double> & outputX, std::vector<double> & outputY)
	{
		int num = inputX.size();
		std::vector<double> numArray(num);
		numArray[0] = 0;
		double num2 = 0;
		for (int i = 1; i < num; i++)
		{
			double num4 = inputX[i] - inputX[i - 1];
			double num5 = inputY[i] - inputY[i - 1];
			double num6 = sqrt(((num4 * num4) + (num5 * num5)));
			num2 += num6;
			numArray[i] = num2;
		}
		snakelength = min(max(snakelength, fGap), num2);
		double num7 = snakelength / fGap;

		int num8 = num7;
		double num9 = num7 - num8;
		//2.x段长度，会分成3段，需4个点。但2.0段只会分成2段，需3个点
		outputCount = ((int)ceil((double)num7)) + 1;

		std::vector<double> numArray2(outputCount);
		numArray2[0] = 0;
		for (int j = 1; j < (outputCount - 1); j++)
		{
			numArray2[j] = numArray2[j - 1] + fGap;
		}
		numArray2[outputCount - 1] = numArray2[outputCount - 2] + (fGap * num9);

		outputX.resize(outputCount);
		outputY.resize(outputCount);

		CubicSpline spline;
		spline.FitAndEval(numArray, inputX, numArray2, outputCount, outputX);
		CubicSpline spline2;
		spline2.FitAndEval(numArray, inputY, numArray2, outputCount, outputY);
	}

	static void NormalizeVector(double &dx, double &dy)
	{
		double d = sqrt(dx * dx + dy * dy);

		if (d > esp3) // probably not conservative enough, but catches the (0,0) case at least
		{
			dx = dx / d;
			dy = dy / d;
		}
	}

#pragma endregion
};

