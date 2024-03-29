﻿async function createListTimeline(elementId, listId, heightInPixels, maxTweetCount)
{
	var dataSource = {
		sourceType: 'list',
		id: listId
	};

	await createTimeline(elementId, dataSource, heightInPixels, maxTweetCount);
}

function createProfileTimeline(elementId, userName, heightInPixels, maxTweetCount)
{
	var dataSource = {
		sourceType: 'profile',
		screenName: userName
	};

	createTimeline(elementId, dataSource, heightInPixels, maxTweetCount);
}

function createTimeline(elementId, dataSource, heightInPixels, maxTweetCount)
{
	(function ()
	{
		var target = document.getElementById(elementId);

		// See https://dev.twitter.com/web/embedded-timelines/parameters
		var options = {
			chrome: 'noheader nofooter noscrollbar transparent noborders',
		};

		if (heightInPixels)
			options.height = heightInPixels;

		if (maxTweetCount)
			options.tweetLimit = maxTweetCount;

		twttr.ready(function (twttr)
		{
			target.innerHTML = null;
			twttr.widgets.createTimeline(dataSource, target, options);
		});

		if (!twttr.widgets || !target || !twttr.ready || !twttr.ready())
		{
			setTimeout(createTimeline, 250, target, dataSource, heightInPixels, maxTweetCount);
		}
		else
		{
			twttr.widgets.createTimeline(dataSource, target, options);
		}
	})();
}

function initializeTwitter()
{
	// First, include the twitter widget.js before anything that might require
	// it. The docs recommend including it directly in the page template

	window.twttr = (function (d, s, id)
	{
		var js, fjs = d.getElementsByTagName(s)[0],
			t = window.twttr || {};
		if (d.getElementById(id)) return t;
		js = d.createElement(s);
		js.id = id;
		js.src = "https://platform.twitter.com/widgets.js";
		fjs.parentNode.insertBefore(js, fjs);

		t._e = [];
		t.ready = function (f)
		{
			t._e.push(f);
		};

		return t;
	}(document, "script", "twitter-wjs"));
}

initializeTwitter();