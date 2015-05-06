public class Account {

  public string username, email, id;

  public User user;

  private string password;

  public Account (string name, string account_email, string account_id) {
    username = name;
    email = account_email;
    id = account_id;
  }

  public void SetPassword(string pass) {
    password = pass;
  }

  public bool IsRightPassword(string pass) {
    return (password == pass);
  }

  public void CreateUser(string handle, string user_id) {
    user = new User(handle, user_id);
  }

}
