using DalApi;
using DO;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalUser : IUser
{
    DataSource dataSource = DataSource.Instance;

    #region Add order
    /// <summary>
    /// Addes a new user.
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns>The ID of the new user.</returns>
    public int Add(User newUser)
    {
        newUser.UniqID = DataSource.Config.OrderID;
        dataSource.users.Add(newUser);
        return newUser.UniqID;
    }
    #endregion

    #region Delete user
    /// <summary>
    /// Delete an user by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Delete(int ID)
    {
        if (dataSource.users.RemoveAll(user => user?.UniqID == ID) == 0)// Remove the user by ID and if the user does not exists throw an exception.
            throw new DoesNotExistException("User", ID);
    }
    #endregion

    #region Get all users
    /// <summary>
    /// Gets all the users.
    /// </summary>
    /// <returns>An array that refers to all the users.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public IEnumerable<DO.User?> GetAll(Func<DO.User?, bool>? func = null)
    {
        // If there is no users.
        if (dataSource.users.Count == 0)
            throw new EmptyException("users");
        if (func == null)
            return from user in dataSource.users
                   where user != null
                   select user;
        else
            return from user in dataSource.users
                   where func(user)
                   select user;
    }
    #endregion

    #region Get user by func
    /// <summary>
    ///  Gets an user by a boolyan deligate.
    /// </summary>
    /// <param name="func"></param>
    /// <returns>The requested user.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public DO.User GetByFunc(Func<DO.User?, bool> func)
    {
        return dataSource.users?.First(func)
            ?? throw new DoesNotExistException("user");
    }
    #endregion

    #region Get user by ID
    /// <summary>
    /// Gets an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested user.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public DO.User GetByID(int ID)
    {
        return dataSource.users?.Find(user => user?.UniqID == ID)
           ?? throw new DoesNotExistException("user", ID);
    }
    #endregion

    #region Update user
    /// <summary>
    /// updates details of a spesific user.
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Update(DO.User updatedUser)
    {
        int i = 0;
        foreach (DO.User? user in dataSource.users)// Find the requested user.
        {
            if (user?.UniqID == updatedUser.UniqID)// If the user was found.
            {
                dataSource.users[i] = updatedUser;
                return;
            }
            i++;
        }
        throw new DoesNotExistException("user", updatedUser.UniqID);// If the user was not found.
    }
    #endregion
}
