package intellistock.fyp.pucit.intellistock;


import android.provider.BaseColumns;

/**
 * Created by Alina on 2/2/2015.
 */
public class contractdemo {

    public static abstract class tables{
        public static abstract class MarketSummary implements BaseColumns {
            public static final String TABLE_NAME = "market summary";
            public static final String COLUMN_NAME_ID = "CID";
            public static final String COLUMN_NAME_Symbol= "Symbol";
            public static final String COLUMN_NAME_Prev_close = "Prev Close";
            public static final String COLUMN_NAME_Open = "Open";
            public static final String COLUMN_NAME_Low = "low";
            public static final String COLUMN_NAME_High = "High";
            public static final String COLUMN_NAME_Current = "Current";
            public static final String COLUMN_NAME_Change = "Change";
            public static final String COLUMN_NAME_Volume = "Volume";
            public static final String COLUMN_NAME_Date = "Date";

        }
    }
 public static abstract class commands{
        private static final String TEXT_TYPE = " TEXT";
        private static final String Number_TYPE = "NUMBER";
        private static final String COMMA_SEP = ",";
     private static final String FLOAT_TYPE = "FLOAT";
     private static final String DATE_TYPE = "DATE";
        public abstract class MarketSummary{
            public static final String SQL_CREATE_ENTRIES =
                    "CREATE TABLE " + contractdemo.tables.MarketSummary.TABLE_NAME + " ( " +
                            contractdemo.tables.MarketSummary._ID + " TEXT PRIMARY KEY," +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_ID + TEXT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Symbol + TEXT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Prev_close + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Open + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Low + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_High + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Current + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Change + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Volume + FLOAT_TYPE + COMMA_SEP +
                            contractdemo.tables.MarketSummary.COLUMN_NAME_Date + DATE_TYPE + COMMA_SEP + ")";


        }
    }


}
