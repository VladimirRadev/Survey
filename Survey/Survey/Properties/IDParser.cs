using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Views.Admin {
    public static class IDParser {
        public int qId;

        public static int getqId() {
            return qId;
        }

        public static void setqId() {
            this.qId = qId;
        }
    }
}