namespace Schedule.Infrastracture {
	public static class TransposeMatrix {
		public static List<List<T>> Transpose<T>(this List<List<T>> matrix) {
			if (matrix == null || matrix.Count == 0)
				return new List<List<T>>();

			int rows = matrix.Count;
			int cols = matrix[0].Count;

			var transposed = new List<List<T>>(cols);

			for (int i = 0; i < cols; i++) {
				var column = new List<T>(rows);
				for (int j = 0; j < rows; j++) {
					column.Add(matrix[j][i]);
				}
				transposed.Add(column);
			}

			return transposed;
		}
	}
}
