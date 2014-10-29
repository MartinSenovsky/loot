using System;
using System.Collections.Generic;
using System.Threading;

namespace SULogger.Primitives
{
	public class Signal<TPayload1, TPayload2, TPayload3>
	{
		#region Fields

		private List<Action<TPayload1, TPayload2, TPayload3>> m_listeners;
		private bool m_once = false;
		#endregion Fields

		#region Constructors

		public Signal()
		{
			m_listeners = new List<Action<TPayload1, TPayload2, TPayload3>>();
		}

		#endregion Constructors

		#region Methods

		public void Add(Action<TPayload1, TPayload2, TPayload3> listener)
		{
			m_listeners.Add(listener);
		}

		public void AddOnce(Action<TPayload1, TPayload2, TPayload3> listener)
		{
			m_once = true;
			m_listeners.Add(listener);
		}

		public bool Remove(Action<TPayload1, TPayload2, TPayload3> listener)
		{
			return m_listeners.Remove(listener);
		}

		public void Dispatch(TPayload1 payload1, TPayload2 payload2, TPayload3 payload3)
		{
			for (int i = m_listeners.Count - 1; i >= 0; i--)
			{
				m_listeners[i].Invoke(payload1, payload2, payload3);
				if (m_once)
				{
					Remove(m_listeners[i]);
				}
			}

		}

		#endregion Methods
	}

    public class Signal<TPayload1, TPayload2>
    {
        #region Fields

        private List<Action<TPayload1, TPayload2>>  m_listeners;
		private bool m_once = false;
        #endregion Fields

        #region Constructors

        public Signal()
        {
            m_listeners = new List<Action<TPayload1, TPayload2>>();
        }

        #endregion Constructors

        #region Methods

        public void Add(Action<TPayload1, TPayload2> listener)
        {
            m_listeners.Add(listener);
        }

		public void AddOnce(Action<TPayload1, TPayload2> listener)
		{
			m_once = true;
			m_listeners.Add(listener);
		}

        public bool Remove(Action<TPayload1, TPayload2> listener)
        {
            return m_listeners.Remove(listener);
        }

        public void Dispatch(TPayload1 payload1, TPayload2 payload2)
        {
			for (int i = m_listeners.Count - 1; i >= 0; i--)
			{
				m_listeners[i].Invoke(payload1, payload2);
				if (m_once)
				{
					Remove(m_listeners[i]);
				}
			}
        }

        #endregion Methods
    }

    public class Signal<TPayload>
    {
        #region Fields

        private List<Action<TPayload>>  m_listeners;
		private bool m_once = false;
        #endregion Fields

        #region Constructors

        public Signal()
        {
            m_listeners = new List<Action<TPayload>>();
        }

        #endregion Constructors

        #region Methods

        public void Add(Action<TPayload> listener)
        {
            m_listeners.Add(listener);
        }

		public void AddOnce(Action<TPayload> listener)
		{
			m_once = true;
			m_listeners.Add(listener);
		}

        public bool Remove(Action<TPayload> listener)
        {
            return m_listeners.Remove(listener);
        }

        public void Dispatch(TPayload payload)
        {
            for(int i = m_listeners.Count-1; i >= 0 ; i--)
            {
				m_listeners[i].Invoke(payload);
				if (m_once)
				{
					Remove(m_listeners[i]);
				}
            }
        }

        #endregion Methods
    }

    public class Signal
    {
        #region Fields

        private List<Action>  m_listeners;
	    private bool m_once = false;
	    #endregion Fields

        #region Constructors

        public Signal()
        {
            m_listeners = new List<Action>();
        }

        #endregion Constructors

        #region Methods

        public void Add(Action listener)
        {
            m_listeners.Add(listener);
        }

	    public void AddOnce(Action listener)
	    {
		    m_once = true;
			m_listeners.Add(listener);
	    }

        public bool Remove(Action listener)
        {
            return m_listeners.Remove(listener);
        }


	    public void RemoveAll()
	    {
		    m_listeners.Clear();
	    }


        public void Dispatch()
        {
			for (int i = m_listeners.Count - 1; i >= 0; i--)
			{
				m_listeners[i].Invoke();
				if (m_once)
				{
					Remove(m_listeners[i]);
				}
			}
        }

	    #endregion Methods
    }
}
