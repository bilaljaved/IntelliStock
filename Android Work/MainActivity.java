package intellistock.fyp.pucit.intellistock;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.IBinder;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;
import org.json.JSONObject;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.ComponentName;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.os.Messenger;
import android.view.Menu;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.lang.annotation.Annotation;
import java.util.ArrayList;

@SuppressLint("HandlerLeak")
public class MainActivity extends Activity {


    public String intro  = null ;


    private DrawerLayout mDrawerLayout;
    private ListView mDrawerList;
    private ActionBarDrawerToggle mDrawerToggle;

    // nav drawer title
    private CharSequence mDrawerTitle;

    // used to store app title
    private CharSequence mTitle;

    // slide menu items
    private String[] navMenuTitles;


    private ArrayList<NavDrawerItem> navDrawerItems;
    private NavDrawerListAdapter adapter;



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


        @Override
        protected void onCreate(Bundle savedInstanceState)
        {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);



            //Service work...
            Intent intent = null;
            {
                intent = new Intent(this, MyDownloaderService.class);
            }
            intent.putExtra(getResources().getString(R.string.url), "http://172.16.11.197/mySite/api/values");
            intent.putExtra(getResources().getString(R.string.messenger), new Messenger(mHandler));
            startService(intent);
            Utils1 obj=new Utils1();
            String temp=obj.getResult();
            ParseString(temp);
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


            //Nav Bar

            mTitle = mDrawerTitle = getTitle();

            // load slide menu items
            navMenuTitles = getResources().getStringArray(R.array.fragment_list);


            mDrawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
            mDrawerList = (ListView) findViewById(R.id.Slider_menu);

            navDrawerItems = new ArrayList<NavDrawerItem>();

            // adding nav drawer items to array

            navDrawerItems.add(new NavDrawerItem(navMenuTitles[0]));

            navDrawerItems.add(new NavDrawerItem(navMenuTitles[1]));

            navDrawerItems.add(new NavDrawerItem(navMenuTitles[3]));

            navDrawerItems.add(new NavDrawerItem(navMenuTitles[4]));


            // setting the nav drawer list adapter
            adapter = new NavDrawerListAdapter(getApplicationContext(),
                    navDrawerItems);
            mDrawerList.setAdapter(adapter);

            // enabling action bar app icon and behaving it as toggle button
            getActionBar().setDisplayHomeAsUpEnabled(true);
            getActionBar().setHomeButtonEnabled(true);


            mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout,

                    R.string.app_name1, // nav drawer open - description for accessibility
                    R.string.app_name1 // nav drawer close - description for accessibility
            ){
                public void onDrawerClosed(View view) {
                    getActionBar().setTitle(mTitle);
                    // calling onPrepareOptionsMenu() to show action bar icons
                    invalidateOptionsMenu();
                }

                public void onDrawerOpened(View drawerView) {
                    getActionBar().setTitle(mDrawerTitle);
                    // calling onPrepareOptionsMenu() to hide action bar icons
                    invalidateOptionsMenu();
                }
            };
            mDrawerLayout.setDrawerListener(mDrawerToggle);

            if (savedInstanceState == null) {
                // on first time display view for first nav item
                displayView(0);
            }
        }


    /**
     * Slide menu item click listener
     * */
    private class SlideMenuClickListener implements ListView.OnItemClickListener {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position,
                                long id) {
            // display view for selected nav drawer item
            displayView(position);
        }
    }


    public void ParseString(String result)
    {
        try
        {

           // TextView tv = (TextView)findViewById(R.id.my_text_view);
            //TextView tv1 = (TextView)findViewById(R.id.my_text_view1);
            //		tv.setText(introduction);
            JSONObject reader = new JSONObject(result);
            //JSONObject sys= reader.getJSONObject("name");
            String name1 = reader.getString("name");
            int id = reader.getInt("id");
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


        @Override
        public boolean onCreateOptionsMenu(Menu menu) {
            // Inflate the menu; this adds items to the action bar if it is present.
            getMenuInflater().inflate(R.menu.menu_main, menu);
            return true;
        }

        @Override
        public boolean onOptionsItemSelected(MenuItem item) {

            // toggle nav drawer on selecting action bar app title
            if (mDrawerToggle.onOptionsItemSelected(item)) {
                return true;
            }
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


    /***
     * Called when invalidateOptionsMenu() is triggered
     */
    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        // if nav drawer is opened, hide the action items
        boolean drawerOpen = mDrawerLayout.isDrawerOpen(mDrawerList);
        menu.findItem(R.id.action_settings).setVisible(!drawerOpen);
        return super.onPrepareOptionsMenu(menu);
    }
    /**
     * Diplaying fragment view for selected nav drawer list item
     * */
    private void displayView(int position) {
        // update the main content by replacing fragments
    }
    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        // Sync the toggle state after onRestoreInstanceState has occurred.
        mDrawerToggle.syncState();
    }


    }
