package intellistock.fyp.pucit.intellistock;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
/**
 * Created by Alina on 2/2/2015.
 */

    public class MySQLiteOpenHelper extends SQLiteOpenHelper {

        public static final int DATABASE_VERSION = 1;
        public static final String DATABASE_NAME = "Demo.db";

        public MySQLiteOpenHelper(Context context) {
            super(context, DATABASE_NAME , null, DATABASE_VERSION);
            // TODO Auto-generated constructor stub
        }

        @Override
        public void onCreate(SQLiteDatabase arg0) {
            // TODO Auto-generated method stub
            arg0.execSQL(contractdemo.commands.MarketSummary.SQL_CREATE_ENTRIES);
        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            // TODO Auto-generated method stub

        }

    }
