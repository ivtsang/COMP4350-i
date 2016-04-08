$(document).ready(function () {

    $('.create-button').click(function () {
        var category = $('#selectCategory').val();
        if (category == "profile") {
            hidewConferenceResult();
            showProfileResult();
        } else if (category == "conference") {
            hideProfileResult();
            showConferenceResult();
        } else {
            showProfileResult();
            showConferenceResult();
        }
        var keyword = $('#keyword').val();
        var url = 'http://ec2-52-37-252-126.us-west-2.compute.amazonaws.com/api/SearchService/GetSearchResult/?keyword=' + keyword + '&&category=' + category;
        $.ajax({
            method: 'GET',
            context: this,
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            cache: false,
            success: function (data) {
                if (data.profiles != null && data.profiles.length > 0) {
                    hideProfileNotFound();
                    showProfileHeader();
                    removeProfileList();
                    createProfileList(data.profiles);
                } else {
                    hideProfileHeader();
                    removeProfileList();
                    showProfileNotFound();
                }

                if (data.conferences != null && data.conferences.length > 0) {
                    showConferenceHeader();
                    hideConferenceNotFound();
                    removeConferenceList();
                    createConferenceList(data.conferences);
                } else {
                    hideConferenceHeader();
                    removeConferenceList();
                    showConferenceNotFound();
                }
            },
            error: function (err) {
                alert('System error happened. Try again Please');
            }
        });
    });

    function createProfileList(profiles) {
        $.each(profiles, function (index, value) {
            var name = value.FirstName + ' ' + value.LastName;
            var school = value.School;
            var country = value.Country;
            var profileLink = "http://ec2-52-37-252-126.us-west-2.compute.amazonaws.com/Profiles/Details/" + value.ProfileId;
            var row = '<div class="row student"><div class="col-sm-4"><a href="' + profileLink + '"> ' + name + '</a></div><div class="col-sm-4">' + school + '</div><div class="col-sm-4">' + country + '</div></div>'
            $('.profile-search-result').append(row);
        });
    }

    function createConferenceList(conferences) {
        $.each(conferences, function (index, value) {
            var title = value.Title;
            var date = value.Date.substring(0,10);
            var location = value.location;
            var conferenceLink = "http://ec2-52-37-252-126.us-west-2.compute.amazonaws.com/Conference/Details/" + value.ConferenceId;
            var row = '<div class="row conference"><div class="col-sm-4"><a href="' + conferenceLink + '"> ' + title + '</a></div><div class="col-sm-4">' + date + '</div><div class="col-sm-4">' + location + '</div></div>'
            $('.conference-search-result').append(row);
        });
    }

    function hidewConferenceResult() {
        $('.conference-search-result').addClass('hidden');
    }

    function showConferenceResult() {
        $('.conference-search-result').removeClass('hidden');
    }

    function hideProfileResult() {
        $('.profile-search-result').addClass('hidden');
    }

    function showProfileResult() {
        $('.profile-search-result').removeClass('hidden');
    }

    function removeProfileList() {
        $('.profile-search-result > .student').remove();
    }
    function removeConferenceList() {
        $('.conference-search-result > .conference').remove();
    }

    function hideProfileNotFound() {
        $('.empty-profile-list').addClass('hidden');
    }

    function showProfileNotFound() {
        $('.empty-profile-list').removeClass('hidden');
    }

    function hideConferenceNotFound() {
        $('.empty-conference-list').addClass('hidden');
    }
    
    function showConferenceNotFound() {
        $('.empty-conference-list').removeClass('hidden');
    }

    function hideProfileHeader() {
        $('.profile-search-result > .header').addClass('hidden');
    }
    function showProfileHeader() {
        $('.profile-search-result > .header').removeClass('hidden');
    }
    function hideConferenceHeader() {
        $('.conference-search-result > .header').addClass('hidden');
    }
    function showConferenceHeader() {
        $('.conference-search-result > .header').removeClass('hidden');
    }
});