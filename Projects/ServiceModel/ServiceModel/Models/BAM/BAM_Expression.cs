using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Xml.Linq;

namespace ServiceModel.Models.BAM
{
    public class BAM_Expression
    {
        public string Id { get; set; }
        public Expression<Func<Projection, bool>> Criteria { get; set; }
    }

    //public class ValueExpressionLeft
    //{
    //    public string Property { get; set; }
    //}

    //public class ValueExpressionRight
    //{
    //    public string Value { get; set; }
    //}

    //public class SimpleExpression
    //{
    //    public ValueExpressionLeft ValueExpressionLeft { get; set; }
    //    public string Operator { get; set; }
    //    public ValueExpressionRight ValueExpressionRight { get; set; }
    //}

    //public class Expression
    //{
    //    public SimpleExpression SimpleExpression { get; set; }
    //}

    //public class Base
    //{
    //    public Expression Expression { get; set; }
    //}

    //public class Criteria
    //{
    //    public Base Base { get; set; }
    //}
}