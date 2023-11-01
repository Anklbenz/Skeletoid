/*using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class FreeLimits {
   public int messageIncrementTimeMilliseconds, messagesCount;
}

public class ChatSettings {
   public FreeLimits freeLimits;
}

[Serializable]
public class Messenger {
   [SerializeField] private LimitsView limitsView;
   private readonly LimitsCounter _limitsCounter = new();
   private long currentUnix => ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

   public async UniTask Initialize(ChatSettings chatSettings) {
      _limitsCounter.Initialize(chatSettings.freeLimits);
      _limitsCounter.RefreshEvent += OnRefresh;
      
      await _limitsCounter.Refresh();
   }

   private async void SendAsync() {
      if (_limitsCounter.isEmpty) return;
   
      await _limitsCounter.Refresh();
   }

   public async void FixedUpdate_SystemCall() {
      if (!_limitsCounter.isReady || _limitsCounter.isMax) return;

      var timesLeft = TimeSpan.FromMilliseconds(_limitsCounter.timeLeftToNextMilliseconds);

      if (timesLeft.TotalSeconds <= 0)
         await _limitsCounter.Refresh();
      else 
         limitsView.timerText = timesLeft.ToString(@"mm\:ss");
   }

   private void OnRefresh() {
      limitsView.requestsText = _limitsCounter.requestsAvailable.ToString();
      if (_limitsCounter.isMax)
         limitsView.timerText = "Full";
   }
}
//------------------------------------------------------------------------------------------------------------------------
/*public class LimitsCounter {
   public event Action RefreshEvent; 
   public bool isEmpty => _requestsAvailable == 0;
   public bool isMax => _requestsAvailable == _maxRequests;
   public bool isReady { get; private set; }
   public int requestsAvailable => _requestsAvailable;
   public long timeLeftToNextMilliseconds => _nextIterationTimeInLocal - ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
   private DateTime _timeToNextIteration;
   private long _incrementTimeMilliseconds, _timeDelta, _lastUpdateTime, _nextIterationTimeInLocal;
   private int _requestsAvailable, _maxRequests;

   public void Initialize(FreeLimits limits) {

   }

   /*public async UniTask Refresh() {
      isReady = false;
      var limitsRequest = await DatabaseApi.instance.GetActualLimits();

      _lastUpdateTime = limitsRequest.data.lastRequest;
      _requestsAvailable = limitsRequest.data.requestsAvailable;

      _timeDelta = GetLocalFromGlobalTimeDelta(limitsRequest.data.nowUnix);
      _nextIterationTimeInLocal = (_lastUpdateTime + _timeDelta) + _incrementTimeMilliseconds;

      isReady = true;
      RefreshEvent?.Invoke();
   }#2#

   private long GetLocalFromGlobalTimeDelta(long time) =>
      ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - time;
}
//----------------------------------------------------------------------------------------------#1#
public class LimitsView : MonoBehaviour{
   [SerializeField] private TMP_Text timerToNextText, requestsCountText;
   public string timerText { set => timerToNextText.text = value; }
   public string requestsText { set => requestsCountText.text = value; }
   public bool timerVisible {
      set => timerToNextText.gameObject.SetActive(value);
   }
}*/