package intellistock.fyp.pucit.intellistock;

import android.app.ActionBar;
import android.app.Activity;
import android.app.Fragment;
import android.app.Notification;
import android.content.Intent;
import android.content.res.Configuration;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Message;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.support.v7.app.ActionBarDrawerToggle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.SimpleCursorAdapter;
import android.widget.Toast;
import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.client.HttpClient;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

import android.os.Handler;
import android.os.Messenger;
import android.util.Log;
import android.webkit.WebView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;


public class login_activity extends ActionBarActivity {


    public String intro  = null ;
    private ListView mDrawerList;
    private DrawerLayout mDrawerLayout;
    private ArrayAdapter<String> mAdapter;
    private ActionBarDrawerToggle mDrawerToggle;
    private String mActivityTitle;

    private Handler mHandler = new Handler(){
        @Override
        public void handleMessage(Message msg) {
            // TODO Auto-generated method stub
            super.handleMessage(msg);
            intro = (String)msg.obj;

            //		TextView tv = (TextView)findViewById(R.id.my_text_view);
            //		tv.setText(introduction);
        }
    };

    public void loginbtnpressed(View v)
    {
       /* try {
            startActivity(new Intent(login_activity.this, MarketSummary.class));
        }catch(Exception e)
        {
            Log.d("f" , e.getMessage()) ;
        }
*/

    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login_layout);

        // Navigation drawer

        mDrawerList = (ListView)findViewById(R.id.navList);mDrawerLayout = (DrawerLayout)findViewById(R.id.drawer_layout);

        mActivityTitle = getTitle().toString();

        addDrawerItems();
        setupDrawer();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeButtonEnabled(true);


      /*  //Service work...
        Intent intent = null;
        {
            intent = new Intent(this, MyDownloaderService.class);
        }
        intent.putExtra(getResources().getString(R.string.url), "http://192.168.0.6/IntelliStock/api/values");
        intent.putExtra(getResources().getString(R.string.messenger), new Messenger(mHandler));
        startService(intent);
        Utils1 obj=new Utils1();
        intro=obj.getResult();
        ParseString(intro);
        // it is a welcome screen activity
        //tasks to be done during loading phase of app.

        // create DB (All tables and their columns as mentioned in ERD)
        // start service here,
        // service will fetch data of market into sqlite tables.

        // fire an intent to go to login activity, after all data from server is loaded.

        SQLiteDatabase db = new MySQLiteOpenHelper(this).getReadableDatabase();
        String table = contractdemo.tables.MarketSummary.TABLE_NAME;
        String[] columns = {contractdemo.tables.MarketSummary._ID, contractdemo.tables.MarketSummary.COLUMN_NAME_ID,
                contractdemo.tables.MarketSummary.COLUMN_NAME_Symbol, contractdemo.tables.MarketSummary.COLUMN_NAME_Prev_close,
                contractdemo.tables.MarketSummary.COLUMN_NAME_Open, contractdemo.tables.MarketSummary.COLUMN_NAME_Low,
                contractdemo.tables.MarketSummary.COLUMN_NAME_High, contractdemo.tables.MarketSummary.COLUMN_NAME_Current,
                contractdemo.tables.MarketSummary.COLUMN_NAME_Change, contractdemo.tables.MarketSummary.COLUMN_NAME_Volume,
                contractdemo.tables.MarketSummary.COLUMN_NAME_Date,};
        String selection = null;
        String[] selectionArgs = null;
        String groupBy = null;
        String having = null;
        String orderBy = null;
        Cursor c = db.query(table, columns, selection, selectionArgs, groupBy, having, orderBy);
        int[] to = {android.R.id.text1};
        String[] from = {contractdemo.tables.MarketSummary.COLUMN_NAME_ID};
        SimpleCursorAdapter sca = new SimpleCursorAdapter(this, android.R.layout.simple_list_item_1, c, from, to, SimpleCursorAdapter.FLAG_REGISTER_CONTENT_OBSERVER);
        // setListAdapter(sca);



    public void ParseString(String result)
    {
        try
        {

            // TextView tv = (TextView)findViewById(R.id.my_text_view);
            //TextView tv1 = (TextView)findViewById(R.id.my_text_view1);
            //		tv.setText(introduction);
            JSONObject reader = new JSONObject(result);
            //JSONObject sys= reader.getJSONObject("name");
            String name1 = reader.getString("username");
            int id = reader.getInt("password");
            Toast.makeText(this, name1+ " " + id , Toast.LENGTH_LONG).show();
            // tv.setText(name1);
            //tv1.setText(""+id);
        }
        catch (Exception e)
        {

            // TODO Auto-generated catch block
            //ad.setTitle("Failure");
            //ad.setMessage(e.toString());
        }
    }



    /// Login button pressed..

    public void  loginbtnpressed(View v)
    {
      /*  // get the username and password entered by user.
       String username1 = ((EditText)findViewById(R.id.editText1)).getText().toString() ;
      String password1 = ((EditText)findViewById(R.id.editText2)).getText().toString() ;

        // call the service method to go to server alongwith username and password and return us the status, true or false.

        HttpClient Client = new DefaultHttpClient();
        String URL="http:///mySite/api/values/getLogin?username="+username1+"&password="+password1;
        try {
            String SetServerString = "";
            HttpGet httpget = new HttpGet(URL);
            ResponseHandler<String> responseHandler = new BasicResponseHandler();
            SetServerString = Client.execute(httpget, responseHandler);


            Toast.makeText(getBaseContext(), "posted to internet", Toast.LENGTH_SHORT).show();
        }
        catch(Exception ex)
        {
            Toast.makeText(getBaseContext(), "not posted to internet", Toast.LENGTH_SHORT).show();

        }
*/

       /* Intent i = new Intent(login_activity.this , TradeScreen.class);
        startActivity(i) ;*/

    }

    private void addDrawerItems() {
        String[] osArray = { "Trade Screen", "Market Summary", "Market History", "Market Predictions" , "Previous Trends" , "Settings" , "Help" , "About Developer" };
        mAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, osArray);
        mDrawerList.setAdapter(mAdapter);

        mDrawerList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                switch (position)
                {
                   // Trade screen option
                    case 0 : Intent ts = new Intent(login_activity.this , TradeScreen.class);
                             startActivity(ts);
                            break;

                   // Market summary option
                    case 1 : Intent ms = new Intent(login_activity.this, MarketSummary.class);
                             startActivity(ms);
                             break ;

                    // Market history
                    case 2 :
                        Intent mh = new Intent(login_activity.this , MarketHistory.class);
                        startActivity(mh);
                        break ;


                    // Market Predictions
                    case 3:
                        Intent sp = new Intent(login_activity.this , SystemPrediction.class);
                        startActivity(sp);

                        break ;

                    // Previous trends

                    case 4: Intent pt = new Intent(login_activity.this , PreviousTrends.class);
                        startActivity(pt);

                        break ;

                    // Settings
                    case 5:
                        Intent set = new Intent(login_activity.this , Settings.class);
                        startActivity(set);
                        break ;

                    // Help
                    case 6:
                        Intent help = new Intent(login_activity.this , Help.class);
                        startActivity(help);
                        break ;

                    // About developer
                    case 7:
                        Intent abt = new Intent(login_activity.this , AboutDeveloper.class);
                        startActivity(abt);
                        break ;


                }


            }
        });
    }

    private void setupDrawer() {
        mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.drawer_open, R.string.drawer_close) {

            /** Called when a drawer has settled in a completely open state. */
            public void onDrawerOpened(View drawerView) {
                super.onDrawerOpened(drawerView);
                getSupportActionBar().setTitle("IntelliStock");
                invalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
            }

            /** Called when a drawer has settled in a completely closed state. */
            public void onDrawerClosed(View view) {
                super.onDrawerClosed(view);
                getSupportActionBar().setTitle(mActivityTitle);
                invalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
            }
        };

        mDrawerToggle.setDrawerIndicatorEnabled(true);
        mDrawerLayout.setDrawerListener(mDrawerToggle);
    }

    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        // Sync the toggle state after onRestoreInstanceState has occurred.
        mDrawerToggle.syncState();
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        mDrawerToggle.onConfigurationChanged(newConfig);
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

        // Activate the navigation drawer toggle
        if (mDrawerToggle.onOptionsItemSelected(item)) {
            return true;
        }


        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
