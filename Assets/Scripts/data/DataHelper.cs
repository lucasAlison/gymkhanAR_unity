using UnityEngine;

namespace DataHelpers
{
    public class DataHelper
    {
        private static DataHelper instance;
        public static DataHelper getInstance()
        {
            if (instance == null) instance = new DataHelper();
            return instance;
        }

        private string token; 
        /* *** caches *** */
        private Level level;
        private Tip tip;
        private Tracking tracking;
        private Question question;        

        public Level getLevel() 
        {
            if (level == null || !level.hasNextLevel())
            {
                updateLevel();
            }

            return level;
        }

        public Tip getTip()
        {
            if (level == null) updateLevel();            
            if (tip == null)
            {
                tip = HttpHelper.getTip(token, level.tipId);
            }
            return tip;
        }
        
        public Tracking getTracking()
        {
            if (level == null) updateLevel();

            if (tracking == null)
            {
                tracking = HttpHelper.getTracking(token, level.trackingId);
            }
            return tracking;
        }
        
        public Question getQuestion()
        {
            if (level == null) updateLevel();

            if (question == null)
            {
                question = HttpHelper.getQuestion(token, level.questionId);
                question.respondeu = false;
            }
            return question;
        }

        public void setToken(string token)
        {
            this.token = token;
        }

        public string getToken()
        {
            return this.token;
        }

        private void updateLevel()
        {
            if (token == null) return;
            level = HttpHelper.getLevel(token);
        }
    }   
}