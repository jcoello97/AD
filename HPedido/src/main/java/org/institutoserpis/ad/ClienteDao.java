package org.institutoserpis.ad;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class ClienteDao {
	private static EntityManagerFactory entityManagerFactory = 
			Persistence.createEntityManagerFactory("org.institutoserpis.ad.hpedido");
	
	public static void createCliente(String nombre){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Cliente cliente = new Cliente();
		cliente.setNombre(nombre);
		
		entityManager.persist(cliente);
		
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	public static List<Cliente> getListClientes(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Cliente> clientes = entityManager.createQuery(
			"from Cliente", Cliente.class).getResultList();
		
		entityManager.getTransaction().commit();
		entityManager.close();
		return clientes;
	}
	public static void removeCliente (Cliente cliente){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.remove(cliente);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	public static boolean existCliente (long id){
		List<Cliente> clientes = getListClientes();
		for (Cliente cliente : clientes) {
			if (cliente.getId() == id) {
				return true;
			}
		}
		return false;
	}
	public static void updateCliente (Cliente cliente){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.refresh(cliente);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	public static Cliente getCliente (long id){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Cliente cliente = entityManager.createQuery(
			"select c from Cliente c where c.id = :id", Cliente.class)
				.setParameter("id", id)
				.getSingleResult();
		
		entityManager.getTransaction().commit();
		entityManager.close();
		return cliente;
	}
}
