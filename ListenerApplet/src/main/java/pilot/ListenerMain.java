package pilot;

import java.applet.Applet;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.security.AccessController;
import java.security.PrivilegedAction;

import netscape.javascript.JSObject;

public class ListenerMain extends Applet {
	private static final long serialVersionUID = 1L;
	private Object callbacks;

	@Override
	public void start() {
	}

	public void listen(final int port, Object callbacks) {

		this.callbacks = callbacks;

		System.out.println("Before new Thread");
		new Thread( new Runnable()  {

			@Override
			public void run() {

				System.out.println("Open socket...");
				try (
						ServerSocket serverSocket = new ServerSocket(port);
						Socket clientSocket = serverSocket.accept();
						PrintWriter out = new PrintWriter(clientSocket.getOutputStream(), true);
						BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
					) {
					System.out.println("start while");
					while(true) {
						String message = (String) in.readLine();
						if (message.equals(".")) break;
						out.print(new StringBuilder(message).reverse());
						out.flush();
					}
					System.out.println("end while");
				} 
				catch (Exception e) {
					e.printStackTrace();
				}
			}
		}).start();
		
		System.out.println("Exit");
	}

	public void doProcessing(final String message) {
		final Applet app = this;

		Thread async = new Thread() {
			public void run() {
				System.out.println("message: " + message);
				JSObject w = JSObject.getWindow(app);
				w.call("ProcessingCallback", new Object[] { true, message,
						callbacks });
			}
		};

		async.start();
	}

}
