public class Application {

  public Account current_account;
  public Server server;

  public Application () {

    if (AppValues.ONLINE) {
      server = new Server(AppValues.SERVER_ADDR,AppValues.SERVER_PORT);
    }


  }

}
