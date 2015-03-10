package intellistock.fyp.pucit.intellistock;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.widget.Toast;
//import service.webservice.webservice1.R;




public class Utils1
{
	static String result;
	public static void download(Context c, Intent i)
	{
		String urlString = i.getStringExtra(c.getResources().getString(R.string.url));
		try{
			new RequestTask().execute(urlString);
		}
		catch(Exception ex)
		{
			
		}
		Messenger messenger = (Messenger)i.getExtras().get(c.getResources().getString(R.string.messenger));
		//String introduction = Utils.getAsStringFromURL(urlString);
		Message message = Message.obtain();
		message.obj = result;
		
		try {
			messenger.send(message);
		} catch (RemoteException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	public static class RequestTask extends AsyncTask<String, String, String>
	{
		//String result=null;
		/*@Override
		protected void onPreExecute() 
		{
		    super.onPreExecute();
		}*/
	    @Override
	    protected String doInBackground(String... uri) 
	    {


            HttpClient httpclient = new DefaultHttpClient();
	        HttpResponse response;
	        String responseString="";
	        try 
	        {
	            response = httpclient.execute(new HttpGet(uri[0]));
	            StatusLine statusLine = response.getStatusLine();
	            if(statusLine.getStatusCode() == HttpStatus.SC_OK)
	            {
	            	ByteArrayOutputStream out = new ByteArrayOutputStream();
	                response.getEntity().writeTo(out);
	                out.close();
	                //responseString=new String(out.toByteArray(),"java.nio.charset.StandardCharsets.UTF_8");                	                
	                responseString=out.toString();
	            } 
	            else
	            {
	                //Closes the connection.	            	
	                response.getEntity().getContent().close();
	                throw new IOException(statusLine.getReasonPhrase());
	               
	            }
	        }
	        catch (Exception e) 
	        {
	        	//Toast.makeText(getBaseContext(), e.toString(), Toast.LENGTH_LONG).show();
	        }
	        return responseString;
	    }

	    @Override
	    protected void onPostExecute(String resu) 
	    {
	        super.onPostExecute(resu);
	        result=resu;
	      //Toast.makeText(getBaseContext(), result, Toast.LENGTH_LONG).show();
	       //return result;
			/*try 
			{				
				t1.setText(result);							
				JSONObject reader = new JSONObject(result);
				//JSONObject sys= reader.getJSONObject("name");
				 String name1 = reader.getString("name");
		         int id = reader.getInt("id");		        
		         t2.setText(name1+" & "+id);
			} 
			catch (Exception e) 
			{
				t2.setText(e.toString());
				// TODO Auto-generated catch block
				//ad.setTitle("Failure");
				//ad.setMessage(e.toString());
			}
			*/
			//t1.setText(id);
	        
	        
	        //ad.setTitle("Success");
	   	 	//ad.setMessage("It worked.");
	        //Do anything with response..
	    }
	    
	}
	public String getResult()
    {
    	return result;
    }
}

