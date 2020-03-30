using System;

namespace AppStoreScraperModels
{
    public class Review
    {
        public long id { get; set; }
        public string userName { get; set; }
        public string userUrl { get; set; }
        public string version { get; set; }
        public int score { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public DateTime DateLoaded { get; set; }
    }
}

//
// [ { id: '1472864600',
//     userName: 'Linda D. Lopez',
//     userUrl: 'https://itunes.apple.com/us/reviews/id324568166',
//     version: '1.80.1',
//     score: 5,
//     title: 'Great way to pass time or unwind',
//     text: 'I was a fan of Bejeweled many moons ago...',
//     url: 'https://itunes.apple.com/us/review?id=553834731&type=Purple%20Software' },,
// { id: '1472864708',
// userName: 'Jennamaxkidd',
// userUrl: 'https://itunes.apple.com/us/reviews/id223990784',
// version: '1.80.1',
// score: 1,
// title: 'Help! THE PROBLEM IS NOT FIXED!',
// text: 'STILL HAVING THE SAME ISSUE.  It\'s happening again...',
// url: 'https://itunes.apple.com/us/review?id=553834731&type=Purple%20Software' },
// (...)
// ]