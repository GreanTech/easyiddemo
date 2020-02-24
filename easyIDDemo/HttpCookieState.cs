using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyIDDemo
{
    public abstract class HttpCookieState
    {
        private readonly string name;
        protected HttpCookieState(string name)
        {
            this.name = name;
        }

        public void SetState(HttpResponse response, string value)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var c = response.Cookies[this.name];
            if (c != null)
            {
                c.Value = value;
            }
            else
            {
                c = new HttpCookie(this.name, value)
                {
                    HttpOnly = true
                };

                response.Cookies.Add(c);
            }
        }

        public string GetState(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Cookies[this.name]?.Value;
        }
    }

    public abstract class BooleanState : HttpCookieState
    {
        private readonly Boolean defaultValue;
        protected BooleanState(string name, bool defaultValue) : base(name)
        {
            this.defaultValue = defaultValue;
        }

        public void SetEnabled(HttpResponse response, bool value)
        {
            base.SetState(response, value.ToString().ToLowerInvariant());
        }

        public bool IsEnabled(HttpRequest request)
        {
            var state = base.GetState(request);
            if (String.IsNullOrWhiteSpace(state) || !Boolean.TryParse(state, out var enabled))
            {
                return this.defaultValue;
            }

            return enabled;
        }
    }

    public class AuthMethodState : HttpCookieState
    {
        public AuthMethodState() : base("authMethod") { }
    }

    public class LanguageState : HttpCookieState
    {
        public LanguageState() : base("language") {}

        public static string BrowserLanguage = "__";
    }

    public class EstablishSsoSessionState : BooleanState
    {
        public EstablishSsoSessionState() : base("establishSsoSession", true) { }
    }
}