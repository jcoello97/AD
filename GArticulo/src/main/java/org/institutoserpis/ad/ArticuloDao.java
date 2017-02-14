package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class ArticuloDao {
	private static Connection connection;
	public static void init(Connection connection) {
		ArticuloDao.connection = connection;
	}
	
	public static Articulo get(long id) {
		return null;
	}
	
	private static String insertSql = 
		"insert into articulo (nombre, precio, categoria) values (?, ?, ?)";
	private static PreparedStatement insertPreparedStatement;
	private static void insert(Articulo articulo) throws SQLException {
		if (insertPreparedStatement == null)
			insertPreparedStatement = connection.prepareStatement(insertSql);
		insertPreparedStatement.setObject(1, articulo.getNombre());
		insertPreparedStatement.setObject(2, articulo.getPrecio());
		insertPreparedStatement.setObject(3, articulo.getCategoria());
		insertPreparedStatement.executeUpdate();
	}
	
	private static void update(Articulo articulo) {
		
	}
	
	public static void save(Articulo articulo) throws SQLException {
		if (articulo.getId() == 0)
			insert(articulo);
		else
			update(articulo);
	}
	
	public static void delete(long id) {
		
	}
	
	private static String selectSql = "select * from articulo";
	public static List<Articulo> getList() throws SQLException {
		Statement statement = connection.createStatement();
		ResultSet resultSet = statement.executeQuery(selectSql);
		List<Articulo> articulos = new ArrayList<>();
		while (resultSet.next()){
			Articulo articulo = new Articulo();
			articulo.setId(resultSet.getLong("id"));
			articulo.setNombre(resultSet.getString("nombre"));
			articulo.setPrecio(resultSet.getBigDecimal("precio"));
			articulo.setCategoria(resultSet.getLong("categoria"));
			articulos.add(articulo);
		}
		return articulos;
	}
	
	public static void close() throws SQLException {
		if (insertPreparedStatement != null)
			insertPreparedStatement.close();
	}

}
