package intellistock.fyp.pucit.intellistock;

import android.app.ActionBar;
import android.app.Activity;
import android.app.Notification;
import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;


public class login_activity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login_layout);

    }
    public void  loginbtnpressed(View v)
    {
        // get the username and password entered by user.
     //    String username = ((EditText)findViewById(R.id.editText1)).getText().toString() ;
      //   String password = ((EditText)findViewById(R.id.editText2)).getText().toString() ;

        // call the service method to go to server alongwith username and password and return us the status, true or false.

        // if status is true then go to trade screen.
        // else , display toast that username or pass is incorrect.

       Intent i = new Intent(login_activity.this , TradeScreen.class);
        startActivity(i) ;

    }
    public void signupbtnpressed(View v)
    {
        Intent i = new Intent(login_activity.this , SignUp.class);
        startActivity(i) ;
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_login_activity, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
