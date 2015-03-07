package intellistock.fyp.pucit.intellistock;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;

public class MyDownloaderService extends Service {

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		// TODO Auto-generated method stub
		final Intent i = intent;
		Thread t = new Thread(new Runnable() {
			
			@Override
			public void run() {
				// TODO Auto-generated method stub
				Utils1.download(MyDownloaderService.this, i);					
				MyDownloaderService.this.stopService(i);
			}
		});
		t.start();
		return super.onStartCommand(intent, flags, startId);
	}
	
	@Override
	public IBinder onBind(Intent intent) {
		// TODO Auto-generated method stub
		return null;
	}

}
